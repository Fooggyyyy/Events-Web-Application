using Abp.AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Validators;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Infastructure.Persistence;
using Events_Web_Application.src.WebAPI.Extensions;
using Events_Web_Application.src.WebAPI.Extensions.Token;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Events_Web_Application.src.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            AppDbContext context,
            IJwtService jwtService,
            ILogger<AuthController> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthTokens>> Register([FromBody] RegisterRequest request, [FromServices] IValidator<UserDTO> validator)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user != null)
                return BadRequest("Пользователь с данный почтой уже зарегестрирован");

            if (user == null)
            {

                var userDTO = new UserDTO
                {
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    BirthdayDate = request.BirthdayDate,
                    Participations = new List<Participation>()
                };
                
                var validationResult = await validator.ValidateAsync(userDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                user = new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    BirthdayDate = request.BirthdayDate,
                    IsAdmin = false,
                    Participations = new List<Participation>()
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Создан новый пользователь: {user.Email}");
            }

            var tokens = _jwtService.GenerateTokens(user);

            var refreshToken = new RefreshToken
            {
                Token = tokens.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(tokens);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthTokens>> Login([FromBody] LoginRequest request, [FromServices] IValidator<UserDTO> validator)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Name == request.Name);

            if (user == null)
                return BadRequest("Неккоректный ввод данных или такого пользователя нет");

            var tokens = _jwtService.GenerateTokens(user);

            var refreshToken = new RefreshToken
            {
                Token = tokens.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        [Authorize(Policy = "AtLeastUser")]
        public async Task<ActionResult<AuthTokens>> Refresh([FromBody] RefreshRequest request)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
            var userId = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized("Invalid user");

            var storedRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == userId);

            if (storedRefreshToken == null || storedRefreshToken.IsExpired)
                return Unauthorized("Invalid refresh token");

            var newTokens = _jwtService.GenerateTokens(user);

            _context.RefreshTokens.Remove(storedRefreshToken);

            var newRefreshToken = new RefreshToken
            {
                Token = newTokens.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserId = user.Id
            };

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return Ok(newTokens);
        }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Name {  get; set; }
        public string Surname { get; set; }
        public DateOnly BirthdayDate { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

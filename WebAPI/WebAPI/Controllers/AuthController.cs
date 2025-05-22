using Abp.AutoMapper;
using Application.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Validators;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Infastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Events_Web_Application.src.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthTokens>> Register([FromBody] RegisterRequest request)
        {
            var tokens = await _mediator.Send(new RegisterUserCommand(request.Email, request.Name, request.Surname, request.BirthdayDate));
            return Ok(tokens);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthTokens>> Login([FromBody] LoginRequest request)
        {
            var tokens = await _mediator.Send(new LoginUserCommand(request.Email, request.Name));
            return Ok(tokens);
        }

        [HttpPost("refresh")]
        [Authorize(Policy = "AtLeastUser")]
        public async Task<ActionResult<AuthTokens>> Refresh([FromBody] RefreshRequest request)
        {
            var tokens = await _mediator.Send(new RefreshTokenCommand(request.AccessToken, request.RefreshToken));
            return Ok(tokens);
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

using Application.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Infastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Events.Commands.Handler
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthTokens>
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IValidator<UserDTO> _validator;

        public RegisterUserHandler(AppDbContext context, IJwtService jwtService, IValidator<UserDTO> validator)
        {
            _context = context;
            _jwtService = jwtService;
            _validator = validator;
        }

        public async Task<AuthTokens> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
                throw new ValidationException("Пользователь с такой почтой уже зарегистрирован");


            var userDTO = new UserDTO
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                BirthdayDate = request.BirthdayDate,
                Participations = new List<Participation>()
            };

            var validationResult = await _validator.ValidateAsync(userDTO, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                BirthdayDate = request.BirthdayDate,
                IsAdmin = false,
                Participations = new List<Participation>()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var tokens = _jwtService.GenerateTokens(user);

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = tokens.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserId = user.Id
            });

            await _context.SaveChangesAsync(cancellationToken);

            return tokens;
        }
    }
}

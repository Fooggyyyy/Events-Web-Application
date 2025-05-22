using Application.Application.Events.Commands.Command;
using Events_Web_Application.src.Infastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthTokens>
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public LoginUserHandler(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthTokens> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.Name == request.Name, cancellationToken);

        if (user == null)
            throw new ValidationException("Некорректный ввод данных или такого пользователя нет");

        var tokens = _jwtService.GenerateTokens(user);

        var refreshToken = new RefreshToken
        {
            Token = tokens.RefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            UserId = user.Id
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);

        return tokens;
    }
}

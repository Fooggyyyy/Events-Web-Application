using Application.Application.Events.Commands.Command;
using Events_Web_Application.src.Infastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthTokens>
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public RefreshTokenHandler(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthTokens> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
        var userId = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));

        var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid user");

        var storedRefreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == userId, cancellationToken);

        if (storedRefreshToken == null || storedRefreshToken.IsExpired)
            throw new UnauthorizedAccessException("Invalid refresh token");

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
        await _context.SaveChangesAsync(cancellationToken);

        return newTokens;
    }
}

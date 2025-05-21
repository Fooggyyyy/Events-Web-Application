using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Domain.interfaces;
using Events_Web_Application.src.Infastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Events_Web_Application.src.Application.Events.Commands.Handler
{
    public class CancelParticipationUserCommandHandler : IRequestHandler<CancelParticipationUserCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CancelParticipationUserCommandHandler(
    AppDbContext context,
    IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(CancelParticipationUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isAdmin = _httpContextAccessor.HttpContext.User.IsInRole("Admin");

            var participation = await _context.Participations
                .FirstOrDefaultAsync(p =>
                    p.EventId == request.EventId &&
                    p.UserId == currentUserId,
                cancellationToken);

            if (participation == null && !isAdmin)
            {
                throw new UnauthorizedAccessException("Вы не зарегистрированы на это событие");
            }

            if (isAdmin && participation == null)
            {
                participation = await _context.Participations
                    .FirstOrDefaultAsync(p => p.EventId == request.EventId, cancellationToken);

                if (participation == null)
                {
                    throw new Exception("На это событие нет регистраций");
                }
            }

            _context.Participations.Remove(participation);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

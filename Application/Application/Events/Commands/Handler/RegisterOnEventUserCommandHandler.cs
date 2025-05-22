using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Handler
{
    public class RegisterOnEventUserCommandHandler : IRequestHandler<RegisterOnEventUserCommand, Unit>
    {
        private IUserRepository _userRepository;

        public RegisterOnEventUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterOnEventUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.RegisterOnEventAsync(request.EventId, request.UserId, cancellationToken);
            return Unit.Value;
        }
    }
}

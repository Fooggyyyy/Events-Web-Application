using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Command
{
    public class RegisterOnEventUserCommand : IRequest<Unit>
    {
        public int EventId;
        public int UserId;

        public RegisterOnEventUserCommand(int eventId, int userId)
        {
            EventId = eventId;
            UserId = userId;
        }
    }
}

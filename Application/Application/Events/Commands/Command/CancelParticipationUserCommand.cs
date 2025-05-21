using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Command
{
    public class CancelParticipationUserCommand : IRequest<Unit>
    {
        public int EventId;

        public CancelParticipationUserCommand(int eventId)
        {
            EventId = eventId;
        }
    }
}

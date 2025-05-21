using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Command
{
    public class DeleteEventCommand : IRequest<Unit>
    {
        public int id;

        public DeleteEventCommand(int id)
        {
            this.id = id;
        }
    }
}

using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;


namespace Events_Web_Application.src.Application.Events.Commands.Handler
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _eventRepository.DeleteAsync(request.id, cancellationToken);
            return Unit.Value;
        }
    }
}

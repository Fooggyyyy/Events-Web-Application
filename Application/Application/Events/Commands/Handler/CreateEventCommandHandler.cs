using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Handler
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
    {
        private IEventRepository _eventRepository;
        private IMapper _mapper;

        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var EventEntity = _mapper.Map<Event>(request);
            await _eventRepository.AddAsync(EventEntity, cancellationToken);
            return EventEntity.Id;

        }
    }
}

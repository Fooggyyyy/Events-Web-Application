using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetByIdEventQueryHandler : IRequestHandler<GetByIdEventQuery, EventDTO>
    {
        private IMapper _mapper;
        private IEventRepository _eventRepository;

        public GetByIdEventQueryHandler(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<EventDTO> Handle(GetByIdEventQuery request, CancellationToken cancellationToken)
        {
            var Event = await _eventRepository.GetByIdAsync(request.Id);
            return _mapper.Map<EventDTO>(Event);
        }
    }
}

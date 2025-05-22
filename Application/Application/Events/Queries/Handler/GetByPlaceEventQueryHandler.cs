using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetByPlaceEventQueryHandler : IRequestHandler<GetByPlaceEventQuery, ICollection<EventDTO>>
    {
        private IMapper _mapper;
        private IEventRepository _eventRepository;

        public GetByPlaceEventQueryHandler(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<ICollection<EventDTO>> Handle(GetByPlaceEventQuery request, CancellationToken cancellationToken)
        {
            var Events =  await _eventRepository.GetByPlaceAsync(request.Place, cancellationToken);

            var pagedUsers = Events
    .Skip((request.Page - 1) * request.PageSize)
    .Take(request.PageSize)
    .ToList();

            return _mapper.Map<ICollection<EventDTO>>(pagedUsers);
        }
    }
}

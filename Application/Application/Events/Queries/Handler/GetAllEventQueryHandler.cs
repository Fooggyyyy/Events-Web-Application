using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;
using System.Text.Json;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetAllEventQueryHandler : IRequestHandler<GetAllEventQuery, ICollection<EventDTO>>
    {
        private IEventRepository _eventRepository;
        private IMapper _mapper;

        public GetAllEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<EventDTO>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetAllAsync(cancellationToken);

            var pagedUsers = events
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return _mapper.Map<ICollection<EventDTO>>(pagedUsers);

        }
    }
}

using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetByCategoryEventQueryHandler : IRequestHandler<GetByCategoryEventQuery, ICollection<EventDTO>>
    {
        private IMapper mapper;
        private IEventRepository eventRepository;

        public GetByCategoryEventQueryHandler(IMapper mapper, IEventRepository eventRepository)
        {
            this.mapper = mapper;
            this.eventRepository = eventRepository;
        }

        public async Task<ICollection<EventDTO>> Handle(GetByCategoryEventQuery request, CancellationToken cancellationToken)
        {
            var Events = await eventRepository.GetByCategoryAsync(request.Category);
            return mapper.Map<ICollection<EventDTO>>(Events);
        }
    }
}

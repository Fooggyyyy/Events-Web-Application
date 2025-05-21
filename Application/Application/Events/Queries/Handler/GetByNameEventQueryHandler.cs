using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetByNameEventQueryHandler : IRequestHandler<GetByNameEventQuery, EventDTO>
    {
        private IMapper mapper;
        private IEventRepository repository;

        public GetByNameEventQueryHandler(IMapper mapper, IEventRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<EventDTO> Handle(GetByNameEventQuery request, CancellationToken cancellationToken)
        {
            var Event = await repository.GetByNameAsync(request.Name);
            return mapper.Map<EventDTO>(Event);
        }
    }
}

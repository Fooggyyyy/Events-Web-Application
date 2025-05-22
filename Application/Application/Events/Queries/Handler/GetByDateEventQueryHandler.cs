using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetByDateEventQueryHandler : IRequestHandler<GetByDateEventQuery, ICollection<EventDTO>>
    {
        private IMapper mapper;
        private IEventRepository repository;

        public GetByDateEventQueryHandler(IMapper mapper, IEventRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<ICollection<EventDTO>> Handle(GetByDateEventQuery request, CancellationToken cancellationToken)
        {
            var Events = await repository.GetByDateAsync(request.Date);

            var pagedUsers = Events
    .Skip((request.Page - 1) * request.PageSize)
    .Take(request.PageSize)
    .ToList();

            return mapper.Map<ICollection<EventDTO>>(pagedUsers);
        }
    }
}

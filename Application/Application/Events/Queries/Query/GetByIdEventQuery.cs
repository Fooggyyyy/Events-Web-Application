using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByIdEventQuery : IRequest<EventDTO>
    {
        public int Id;

        public GetByIdEventQuery(int id)
        {
            Id = id;
        }
    }
}

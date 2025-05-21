using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByNameEventQuery : IRequest<EventDTO>
    {
        public string Name;

        public GetByNameEventQuery(string name)
        {
            Name = name;
        }
    }
}

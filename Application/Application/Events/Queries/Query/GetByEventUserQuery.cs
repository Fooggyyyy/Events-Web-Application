using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByEventUserQuery : IRequest<ICollection<UserDTO>>
    {
        public int EventId;

        public GetByEventUserQuery(int eventId)
        {
            EventId = eventId;
        }
    }
}

using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetAllEventQuery : IRequest<ICollection<EventDTO>>
    {

    }
}

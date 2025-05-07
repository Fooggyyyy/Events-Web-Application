using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByDateEventQuery : IRequest<ICollection<EventDTO>>
    {
        public DateTime Date;

        public GetByDateEventQuery(DateTime date)
        {
            Date = date;
        }
    }
}

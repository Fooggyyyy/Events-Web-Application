using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByDateEventQuery : IRequest<ICollection<EventDTO>>
    {
        public DateTime Date;

        public int Page { get; set; }

        public int PageSize { get; set; }

        public GetByDateEventQuery(DateTime date, int page, int pageSize)
        {
            Date = date;
            Page = page;
            PageSize = pageSize;
        }
    }
}

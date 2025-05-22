using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetAllEventQuery : IRequest<ICollection<EventDTO>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public GetAllEventQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}

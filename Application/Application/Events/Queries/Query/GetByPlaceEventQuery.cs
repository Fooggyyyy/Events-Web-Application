using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByPlaceEventQuery : IRequest<ICollection<EventDTO>>
    {
        public string Place;
        public int Page { get; set; }

        public int PageSize { get; set; }

        public GetByPlaceEventQuery(string place, int page, int pageSize)
        {
            Place = place;
            Page = page;
            PageSize = pageSize;
        }
    }
}

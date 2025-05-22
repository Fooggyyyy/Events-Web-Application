using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Enums;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByCategoryEventQuery : IRequest<ICollection<EventDTO>>
    {
        public Category Category;

        public int Page { get; set; }

        public int PageSize { get; set; }

        public GetByCategoryEventQuery(Category category, int page, int pageSize)
        {
            Category = category;
            Page = page;
            PageSize = pageSize;
        }
    }
}

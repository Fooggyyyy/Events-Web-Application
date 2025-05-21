using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Enums;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByCategoryEventQuery : IRequest<ICollection<EventDTO>>
    {
        public Category Category;

        public GetByCategoryEventQuery(Category category)
        {
            Category = category;
        }
    }
}

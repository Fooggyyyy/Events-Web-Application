using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByPlaceEventQuery : IRequest<ICollection<EventDTO>>
    {
        public string Place;

        public GetByPlaceEventQuery(string place)
        {
            Place = place;
        }
    }
}

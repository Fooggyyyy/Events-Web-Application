using Events_Web_Application.src.Application.Events.DTOs;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Query
{
    public class GetByIdUserQuery : IRequest<UserDTO>
    {
        public int UserId;

        public GetByIdUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}

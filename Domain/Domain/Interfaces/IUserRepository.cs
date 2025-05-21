using Domain.Domain.Interfaces;
using Events_Web_Application.src.Domain.Entities;

namespace Events_Web_Application.src.Domain.interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<ICollection<User>> GetByEventAsync(int EventId);
        Task RegisterOnEventAsync(int EventId, int UserId);
        Task CancelParticipationAsync(int EventId, int UserId);
        Task<ICollection<Changes>> GetChanges();


    }
}

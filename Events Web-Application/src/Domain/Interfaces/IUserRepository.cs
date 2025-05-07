using Events_Web_Application.src.Domain.Entities;

namespace Events_Web_Application.src.Domain.interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetByEventAsync(int EventId);
        Task<User> GetByUserIdAsync(int UserId);
        Task RegisterOnEventAsync(int EventId, int UserId);
        Task CancelParticipationAsync(int EventId, int UserId);
        Task<ICollection<Changes>> GetChanges();


    }
}

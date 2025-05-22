using Domain.Domain.Interfaces;
using Events_Web_Application.src.Domain.Entities;

namespace Events_Web_Application.src.Domain.interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<ICollection<User>> GetByEventAsync(int EventId, CancellationToken cancellationToken);
        Task RegisterOnEventAsync(int EventId, int UserId, CancellationToken cancellationToken);
        Task CancelParticipationAsync(int EventId, int UserId, CancellationToken cancellationToken);
        Task<ICollection<Changes>> GetChanges(CancellationToken cancellationToken);


    }
}

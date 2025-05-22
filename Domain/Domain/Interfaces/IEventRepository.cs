using Domain.Domain.Interfaces;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;

namespace Events_Web_Application.src.Domain.interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<ICollection<Event>> GetAllAsync(CancellationToken cancellationToken);
        Task<ICollection<Event>> GetByDateAsync(DateTime Date, CancellationToken cancellationToken);
        Task<ICollection<Event>> GetByPlaceAsync(string Place, CancellationToken cancellationToken);
        Task<ICollection<Event>> GetByCategoryAsync(Category Category, CancellationToken cancellationToken);
        Task<Event> GetByNameAsync(string Name, CancellationToken cancellationToken);
        Task AddAsync(Event Event, CancellationToken cancellationToken);
        Task UpdateAsync(Event @event, string changesText, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task AddPhotoAsync(int id, string PhotoPath, CancellationToken cancellationToken);
    }
}

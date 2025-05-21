using Domain.Domain.Interfaces;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;

namespace Events_Web_Application.src.Domain.interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<ICollection<Event>> GetAllAsync();
        Task<ICollection<Event>> GetByDateAsync(DateTime Date);
        Task<ICollection<Event>> GetByPlaceAsync(string Place);
        Task<ICollection<Event>> GetByCategoryAsync(Category Category);
        Task<Event> GetByNameAsync(string Name);
        Task AddAsync(Event Event);
        Task UpdateAsync(Event @event, string changesText);
        Task DeleteAsync(int id);
        Task AddPhotoAsync(int id, string PhotoPath);
    }
}

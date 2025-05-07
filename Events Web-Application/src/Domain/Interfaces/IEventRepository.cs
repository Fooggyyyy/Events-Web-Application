using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;

namespace Events_Web_Application.src.Domain.interfaces
{
    public interface IEventRepository
    {
        Task<ICollection<Event>> GetAllAsync();
        Task<ICollection<Event>> GetByDateAsync(DateTime Date);
        Task<ICollection<Event>> GetByPlaceAsync(string Place);
        Task<ICollection<Event>> GetByCategoryAsync(Category Category);
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByNameAsync(string Name);
        Task AddAsync(Event Event);
        Task UpdateAsync(Event Event);
        Task DeleteAsync(int id);
        Task AddPhotoAsync(int id, string PhotoPath);
    }
}

using Events_Web_Application.src.Domain.Enums;
using Events_Web_Application.src.Domain.interfaces;
using Events_Web_Application.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Events_Web_Application.src.Application.Events.Commands.Handler;

namespace Events_Web_Application.src.Infastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private AppDbContext appDbContext;
        private IMemoryCache memoryCache;

        public EventRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.memoryCache = memoryCache;
        }
        public EventRepository(AppDbContext appDbContext, IMemoryCache memoryCache)
        {
            this.appDbContext = appDbContext;
            this.memoryCache = memoryCache;
        }
        public async Task<ICollection<Event>> GetAllAsync()
        {
            return await appDbContext.Events.ToListAsync();
        }
        public async Task<ICollection<Event>> GetByDateAsync(DateTime Date)
        {
            return await appDbContext.Events.Where(x => x.Date.Year == Date.Year && x.Date.Month == Date.Month && x.Date.Day == Date.Day).ToListAsync();
        }

        public async Task<ICollection<Event>> GetByPlaceAsync(string Place)
        {
            return await appDbContext.Events.Where(x => x.Place == Place).ToListAsync();
        }

        public async Task<ICollection<Event>> GetByCategoryAsync(Category Category)
        {
            return await appDbContext.Events.Where(x => x.Category == Category).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await appDbContext.Events.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Event> GetByNameAsync(string Name)
        {
            return await appDbContext.Events.Where(x => x.Name == Name).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Event Event)
        {
            await appDbContext.Events.AddAsync(Event); 
            await appDbContext.SaveChangesAsync();
        }

        //Вот сюда Changes
        public async Task UpdateAsync(Event Event)
        {
            appDbContext.Changes.Where(x => x.EventId == Event.Id).ExecuteDelete();
            appDbContext.Changes.Add(new Changes(Event.Id, UpdateEventCommandHandler.Changes));
            UpdateEventCommandHandler.Changes = " ";
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await appDbContext.Events
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            await appDbContext.SaveChangesAsync();
        }

        public async Task AddPhotoAsync(int id, string PhotoPath)
        {
            memoryCache.TryGetValue(id, out PhotoPath);
            var UpdateEvent = await appDbContext.Events.FindAsync(id);
            
            UpdateEvent.PhotoPath.Add(PhotoPath);
            memoryCache.Set(id, PhotoPath, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));//Кэшируем на 10 минут
            await appDbContext.SaveChangesAsync();
        }
    }
}

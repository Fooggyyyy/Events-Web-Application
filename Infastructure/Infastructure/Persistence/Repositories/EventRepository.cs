using Events_Web_Application.src.Domain.Enums;
using Events_Web_Application.src.Domain.interfaces;
using Events_Web_Application.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

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
        public async Task<ICollection<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await appDbContext.Events.AsNoTracking().ToListAsync();
        }
        public async Task<ICollection<Event>> GetByDateAsync(DateTime Date, CancellationToken cancellationToken)
        {
            return await appDbContext.Events.Where(x => x.Date.Year == Date.Year && x.Date.Month == Date.Month && x.Date.Day == Date.Day).AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Event>> GetByPlaceAsync(string Place, CancellationToken cancellationToken)
        {
            return await appDbContext.Events.Where(x => x.Place == Place).AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Event>> GetByCategoryAsync(Category Category, CancellationToken cancellationToken)
        {
            return await appDbContext.Events.Where(x => x.Category == Category).AsNoTracking().ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await appDbContext.Events.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Event> GetByNameAsync(string Name, CancellationToken cancellationToken)
        {
            return await appDbContext.Events.Where(x => x.Name == Name).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task AddAsync(Event Event, CancellationToken cancellationToken)
        {
            await appDbContext.Events.AddAsync(Event); 
            await appDbContext.SaveChangesAsync();
        }

        //Вот сюда Changes
        public async Task UpdateAsync(Event @event, string changesText, CancellationToken cancellationToken)
        {
            appDbContext.Changes
                .Where(x => x.EventId == @event.Id)
                .ExecuteDelete();

            appDbContext.Changes.Add(new Changes(@event.Id, changesText));

            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await appDbContext.Events
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            await appDbContext.SaveChangesAsync();
        }

        public async Task AddPhotoAsync(int id, string PhotoPath, CancellationToken cancellationToken)
        {
            memoryCache.TryGetValue(id, out PhotoPath);
            var UpdateEvent = await appDbContext.Events.FindAsync(id);
            
            UpdateEvent.PhotoPath.Add(PhotoPath);
            memoryCache.Set(id, PhotoPath, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));//Кэшируем на 10 минут
            await appDbContext.SaveChangesAsync();
        }
    }
}

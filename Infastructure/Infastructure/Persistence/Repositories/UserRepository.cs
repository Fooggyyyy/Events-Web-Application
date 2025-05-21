using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Events_Web_Application.src.Infastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext appDbContext;
 
        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ICollection<User>> GetByEventAsync(int EventId)
        {
            return await appDbContext.Users.Where(x => x.Participations.Any(e => e.EventId == EventId)).AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Changes>> GetChanges()
        {
            return await appDbContext.Changes.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int UserId)
        {
            return await appDbContext.Users.Where(x => x.Id == UserId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task RegisterOnEventAsync(int EventId, int UserId)
        {
            var Event = await appDbContext.Events.FindAsync(EventId);

            var User = await appDbContext.Users.FindAsync(UserId);

            var participation = new Participation(EventId, UserId, DateTime.UtcNow);

            await appDbContext.Participations.AddAsync(participation);
            await appDbContext.SaveChangesAsync();
        }
        public async Task CancelParticipationAsync(int EventId, int UserId)
        {
            //MemoryDB не поддерживает Execute, поэтому на этот тест дает ошибку 
            appDbContext.Participations.Where(x => x.UserId == UserId && x.EventId == EventId).ExecuteDelete();
            await appDbContext.SaveChangesAsync();
        }
    }
}

using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Infastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Events_Web_Application.src.WebAPI.Extensions.Token;
namespace Events_Web_Application.src.Infastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private string _connectionString = "Server=sql_server;Database=EventsAppDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;";

        public DbSet<User> Users => Set<User>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Participation> Participations => Set<Participation>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Changes> Changes => Set<Changes>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipationConfiguration());
            modelBuilder.ApplyConfiguration(new ChangesConfiguration());

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

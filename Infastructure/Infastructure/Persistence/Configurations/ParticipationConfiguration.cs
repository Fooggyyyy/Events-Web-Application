using Events_Web_Application.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events_Web_Application.src.Infastructure.Persistence.Configurations
{
    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder.ToTable("Participations");

            builder.HasKey(p => new { p.EventId, p.UserId });

            builder.HasOne(p => p.Event)
                .WithMany(e => e.Participations)
                .HasForeignKey(p => p.EventId);

            builder.HasOne(p => p.User)
                .WithMany(u => u.Participations)
                .HasForeignKey(p => p.UserId);

            builder.Property(p => p.RegistrationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(p => p.RegistrationDate);
        }
    }
}

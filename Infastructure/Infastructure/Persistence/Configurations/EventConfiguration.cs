using Events_Web_Application.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Events_Web_Application.src.Infastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasIndex(e => e.Name);
            builder.HasIndex(e => e.Date);
            builder.HasIndex(e => e.Place);
            builder.HasIndex(e => e.Category);

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Place)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.MaxUser)
                .IsRequired();

            builder.Property(e => e.Category).HasConversion<string>();

            builder.HasMany(e => e.Participations)
                .WithOne(p => p.Event)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.PhotoPath)
            .HasColumnType("NVARCHAR(500)")
            .IsRequired(false);
        }
    }
}

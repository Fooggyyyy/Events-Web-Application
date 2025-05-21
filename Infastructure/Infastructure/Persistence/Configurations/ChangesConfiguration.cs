using Events_Web_Application.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events_Web_Application.src.Infastructure.Persistence.Configurations
{
    public class ChangesConfiguration : IEntityTypeConfiguration<Changes>
    {
        public void Configure(EntityTypeBuilder<Changes> builder)
        {
            builder.ToTable("Changes");

            builder.HasKey(p => p.EventId);

            builder.HasOne(p => p.Event).WithMany().HasForeignKey(p => p.EventId);

            builder.Property(p => p.ChangesInEvent).IsRequired().HasDefaultValue("Без изменений");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatsReservationService.Domain.Events;

namespace SeatsReservationService.Infrastructure.PostgreSql.Configurations
{
    public class EventDetailsConfiguration : IEntityTypeConfiguration<EventDetails>
    {
        public void Configure(EntityTypeBuilder<EventDetails> builder)
        {
            builder.ToTable("event_details");

            builder.HasKey(ed => ed.EventId).HasName("pk_event_details_id");

            builder.Property(ed => ed.EventId)
                .HasConversion(eId => eId.Value, id => new EventId(id))
                .HasColumnName("event_id")
                .IsRequired();

            builder.Property(ed => ed.Capacity)
                .HasColumnName("capacity")
                .IsRequired();

            builder.Property(ed => ed.Description)
                .HasColumnName("description")
                .IsRequired();

            builder
                .HasOne<Event>()
                .WithOne(e => e.Details)
                .HasForeignKey<EventDetails>(ed => ed.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

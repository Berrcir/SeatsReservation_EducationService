using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatsReservationService.Domain.Events;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Infrastructure.PostgreSql.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.HasKey(e => e.Id).HasName("pk_event_id");

            builder.Property(e => e.Id)
                .HasConversion(eId => eId.Value, id => new EventId(id))
                .HasColumnName("event_id")
                .IsRequired();

            builder.Property(e => e.VenueId)
                .HasColumnName("venue_id");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(e => e.Date)
                .HasColumnName("date")
                .IsRequired();

            //builder.Property(e => e.Details)
            //    .HasColumnName("details")
            //    .IsRequired();

            builder
                .HasOne<Venue>()
                .WithMany()
                .HasForeignKey(e => e.VenueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

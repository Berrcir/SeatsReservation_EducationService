using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Infrastructure.PostgreSql.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("seats");

            builder.HasKey(s => s.Id).HasName("pk_seat_id");

            builder.Property(s => s.Id)
                .HasConversion(sId => sId.Value, id => new SeatId(id))
                .HasColumnName("seat_id")
                .IsRequired();

            builder.Property(s => s.VenueId)
                .HasColumnName("vanue_id")
                .IsRequired();

            builder.Property(s => s.RowNumber)
                .HasColumnName("row_number")
                .IsRequired();

            builder.Property(s => s.SeatNumber)
                .HasColumnName("seat_number")
                .IsRequired();
        }
    }
}

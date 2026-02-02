using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatsReservationService.Domain.Reservations;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Infrastructure.PostgreSql.Configurations
{
    public class ReservationSeatConfiguration : IEntityTypeConfiguration<ReservationSeat>
    {
        public void Configure(EntityTypeBuilder<ReservationSeat> builder)
        {
            builder.ToTable("reservation_seats");

            builder.HasKey(r => r.Id).HasName("pk_reservation_seat_id");

            builder.Property(rs => rs.Id)
                .HasConversion(rsId => rsId.Value, id => new ReservationSeatId(id))
                .HasColumnName("reservation_seat_id");

            builder.Property(rs => rs.SeatId)
                .HasConversion(sId => sId.Value, id => new SeatId(id))
                .HasColumnName("seat_id");

            builder.Property(rs => rs.ReservedAt)
                .HasColumnName("reserved_at")
                .IsRequired();

            builder
                .HasOne(rs => rs.Reservation)
                .WithMany(r => r.ReservedSeats)
                .HasForeignKey("reservatuion_id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<Seat>()
                .WithMany()
                .HasForeignKey(rs => rs.SeatId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

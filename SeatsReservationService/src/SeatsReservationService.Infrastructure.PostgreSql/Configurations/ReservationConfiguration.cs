using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatsReservationService.Domain.Events;
using SeatsReservationService.Domain.Reservations;

namespace SeatsReservationService.Infrastructure.PostgreSql.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");

            builder.HasKey(r => r.Id).HasName("pk_reservation_id");

            builder.Property(r => r.Id)
                .HasConversion(rId => rId.Value, id => new ReservationId(id))
                .HasColumnName("reservation_id")
                .IsRequired();

            builder.Property(r => r.EventId)
                .HasConversion(eId => eId.Value, id => new EventId(id))
                .HasColumnName("event_id")
                .IsRequired();

            builder.Property(r => r.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasConversion<string>() // Запись ниже равносильна. У EF есть встроенный конвертор
                //.HasConversion(status => status.ToString(), statusName => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), statusName))
                .HasColumnName("status")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            // Данная настроука не требуется, так как мы все связи в соединительной таблице прокидываем
            /*builder.HasMany(r => r.ReservedSeats)
                .WithOne(rs => rs.Reservation)
                .HasForeignKey("reservation_id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            */
        }
    }
}

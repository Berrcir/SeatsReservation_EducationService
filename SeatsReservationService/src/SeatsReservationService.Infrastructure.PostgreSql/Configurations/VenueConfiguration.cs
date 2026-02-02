using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatsReservationService.Domain.Constants;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Infrastructure.PostgreSql.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.ToTable("venues");

            builder.HasKey(v => v.Id).HasName("pk_venue_id");

            builder.Property(v => v.Id)
                .HasConversion(vId => vId.Value, id => new VenueId(id))
                .HasColumnName("venue_id"); // Это способ хранения типизированного VenueId в БД. Метод позволяет настроить способ записи и считывания из БД объекта VenueId. Сейчас он пишется как Guid, а достается как VenueId

            /*builder.ComplexProperty(v => v.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Prefix)
                    .IsRequired()
                    .HasMaxLength(LengthConstants.LENGTH_50)
                    .HasColumnName(nameof(VenueName.Prefix).ToLowerInvariant());

                nameBuilder.Property(n => n.Name)
                    .IsRequired()
                    .HasMaxLength(LengthConstants.LENGTH_500)
                    .HasColumnName(nameof(VenueName.Name).ToLowerInvariant());
            });
            */

            builder.OwnsOne(v => v.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Prefix)
                    .IsRequired()
                    .HasMaxLength(LengthConstants.LENGTH_50)
                    .HasColumnName(nameof(VenueName.Prefix).ToLowerInvariant());

                nameBuilder.Property(n => n.Name)
                    .IsRequired()
                    .HasMaxLength(LengthConstants.LENGTH_500)
                    .HasColumnName(nameof(VenueName.Name).ToLowerInvariant());
            });

            builder.Property(v => v.SeatsLimit)
                .HasColumnName("seats_limit")
                .IsRequired();

            builder
                .HasMany(v => v.Seats)
                .WithOne()
                .HasForeignKey(s => s.VenueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

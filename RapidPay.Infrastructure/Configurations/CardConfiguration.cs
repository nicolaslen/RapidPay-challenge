using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RapidPay.Domain.Entities;

namespace RapidPay.Infrastructure.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Balance, balance =>
            {
                balance.Property(x => x.Amount)
                    .IsRequired();
            });

            builder.Property(x => x.Name)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.Number)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.ExpirationMonth)
                .IsRequired();

            builder.Property(x => x.ExpirationYear)
                .IsRequired();

            builder.Property(x => x.SecurityCode)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Cards)
                .HasForeignKey(x => x.UserId);

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RapidPay.Domain.Entities;

namespace RapidPay.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Card)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.CardId);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction); //Avoiding cause cycles or multiple cascade paths.

        builder.Property(x => x.UserId)
            .IsRequired();
    }
}
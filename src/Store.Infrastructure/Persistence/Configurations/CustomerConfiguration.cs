using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Customers;
using Store.Domain.ValueObjects;

namespace Store.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            customerId => customerId.Value,
            value => new CustomerId(value));

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        builder.Property(x => x.CompanyName)
            .HasMaxLength(200);

        builder.Ignore(x => x.DisplayName);
        builder.Ignore(x => x.FullName);

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(a => a.Country)
                .HasMaxLength(100)
                .IsRequired();

            address.Property(a => a.Line1)
                .HasMaxLength(200)
                .IsRequired();

            address.Property(a => a.Line2)
                .HasMaxLength(200)
                .IsRequired(false);

            address.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();

            address.Property(a => a.State)
                .HasMaxLength(100)
                .IsRequired();

            address.Property(a => a.ZipCode)
                .HasMaxLength(10)
                .IsRequired();
        });

        builder.Property(x => x.Email)
            .HasConversion(
                email => email.Value,
                value => EmailAddress.Create(value))
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value));

        builder.HasIndex(x => x.PhoneNumber).IsUnique();

        builder.Property(x => x.Identify)
            .HasConversion(
                identify => identify.Value,
                value => Identify.Create(value))
            .HasMaxLength(11)
            .IsRequired();

        builder.HasIndex(x => x.Identify)
            .IsUnique();

        builder.Property(x => x.Active)
            .IsRequired();
    }
}

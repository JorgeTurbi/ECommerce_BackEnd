
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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
            CustomerId => CustomerId.Value,
            value => new CustomerId(value)
        );

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

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
                .HasMaxLength(200);

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


        builder.Property(x => x.Email).HasConversion(
            Email => Email.Value,
            value => EmailAddress.Create(value)!);


        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();


        builder.Property(x => x.PhoneNumber).HasConversion(
            PhoneNumber => PhoneNumber.Value,
            value => PhoneNumber.Create(value)!);

        builder.HasIndex(x => x.PhoneNumber).IsUnique();

        builder.Property(x => x.Identify).HasConversion(
            Identify => Identify.Value,
            value => Identify.Create(value)!)
            .HasMaxLength(11)
            .IsRequired();


        builder.HasIndex(x => x.Identify)
            .IsUnique();

        builder.Property(x => x.Active)
            .IsRequired();





    }
}
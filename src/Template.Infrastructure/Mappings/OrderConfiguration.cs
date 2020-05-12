using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain;

namespace Template.Infrastructure.Mappings
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Events);

            builder.Property(x => x.Id).HasColumnName("order_id").ValueGeneratedOnAdd();
            builder.Property(x => x.CustomerId).IsRequired().HasColumnName("customer_id");

            builder.Metadata.FindNavigation(nameof(Order.OrderItems))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<int>($"{nameof(OrderStatus)}Id").HasColumnName("status_id").IsRequired();
            builder.HasOne(x => x.OrderStatus)
                .WithMany()
                .HasForeignKey($"{nameof(OrderStatus)}Id");

            builder.OwnsOne(x => x.Address, x =>
            {
                x.Property(p => p.City).HasColumnName("city").HasMaxLength(128);
                x.Property(p => p.Country).HasColumnName("country").HasMaxLength(128).IsRequired();
                x.Property(p => p.PostalCode).HasColumnName("postalcode").HasMaxLength(128).IsRequired();
                x.Property(p => p.State).HasColumnName("state").HasMaxLength(128);
                x.Property(p => p.Street).HasColumnName("street").HasMaxLength(256).IsRequired();
            });
        }
    }
}

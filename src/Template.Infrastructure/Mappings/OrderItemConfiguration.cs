using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain;

namespace Template.Infrastructure.Mappings
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.Events);

            builder.Property(x => x.Id).HasColumnName("order_item_id").ValueGeneratedOnAdd();

            builder.Property<long>($"{nameof(Order)}Id").HasColumnName("order_id").IsRequired();

            builder.Property(x => x.ProductId).HasColumnName("product_id").IsRequired();

            builder.Property(x => x.ProductName).HasColumnName("product_name").IsRequired();

            builder.Property(x => x.Quantity).HasColumnName("quantity").IsRequired();

            builder.Property(x => x.Price).HasColumnName("price").IsRequired();

            builder.OwnsOne(x => x.Discount, x =>
            {
                x.Property(p => p.Amount).HasColumnName("discount_amount").IsRequired();
                builder.Property<int>($"{nameof(DiscountType)}Id").HasColumnName("discount_type_id").IsRequired();
                x.HasOne(x => x.DiscountType)
                    .WithMany()
                    .HasForeignKey($"{nameof(DiscountType)}Id");
            });
        }
    }
}

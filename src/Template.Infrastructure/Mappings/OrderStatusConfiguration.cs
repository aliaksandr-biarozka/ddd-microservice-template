using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain;

namespace Template.Infrastructure.Mappings
{
    internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("order_status");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("order_status_id").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Name).HasColumnName("description").HasMaxLength(64).IsRequired();
        }
    }
}

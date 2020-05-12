using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain;

namespace Template.Infrastructure.Mappings
{
    internal class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> builder)
        {
            builder.ToTable("discount_type");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("discount_type_id").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Name).HasColumnName("description").HasMaxLength(64).IsRequired();
        }
    }
}

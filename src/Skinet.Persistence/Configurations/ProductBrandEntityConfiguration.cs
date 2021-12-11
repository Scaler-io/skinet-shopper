using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Entities.Entities;

namespace Skinet.Persistence.Configurations
{
    class ProductBrandEntityConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.HasIndex(pb => pb.Name).IsUnique();
            builder.Property(pb => pb.Name).IsRequired().HasMaxLength(50);
        }
    }
}

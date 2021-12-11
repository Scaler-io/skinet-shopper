using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Entities.Entities;

namespace Skinet.Persistence.Configurations
{
    class ProductTypeEntityConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasIndex(pt => pt.Name).IsUnique();
            builder.Property(pt => pt.Name).IsRequired().HasMaxLength(50);
        }
    }
}

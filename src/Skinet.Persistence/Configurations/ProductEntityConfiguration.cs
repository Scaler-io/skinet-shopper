using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Entities.Entities;

namespace Skinet.Persistence.Configurations
{
    class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50).HasColumnName("ProductName");
        }
    }
}

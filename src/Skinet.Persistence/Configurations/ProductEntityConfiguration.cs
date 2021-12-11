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
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,0)");
            builder.Property(p => p.PictureUrl).IsRequired();

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ProductBrand)
                    .WithMany()
                    .HasForeignKey(p => p.ProductBrandId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

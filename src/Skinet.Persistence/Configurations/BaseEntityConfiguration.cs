using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Entities.Common;

namespace Skinet.Persistence.Configurations
{
    public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.HasKey(b => b.Id).Metadata.IsPrimaryKey();
            builder.HasIndex(b => b.Id).IsUnique();

            builder.Property(b => b.CreatedBy).IsRequired();
            builder.Property(b => b.LastModifiedBy).IsRequired();
            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.LastModifiedAt).IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Entities.Entities.Identity;


namespace Skinet.Persistence.Configurations.IdentityModelconfiguration
{
    public class SkinetUserEntityConfiguration : IEntityTypeConfiguration<SkinetUser>
    {
        public void Configure(EntityTypeBuilder<SkinetUser> builder)
        {
            builder.Property(x => x.DisplayName).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Entities.Entities.Identity;


namespace Skinet.Persistence.Configurations.IdentityModelconfiguration
{
    public class SkinetUserAddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.ZipCode).IsRequired();
            builder.Property(x => x.SkinetUserId).IsRequired();
        }
    }
}

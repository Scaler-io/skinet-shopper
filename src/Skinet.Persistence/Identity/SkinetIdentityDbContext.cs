using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skinet.Entities.Entities.Identity;
using Skinet.Persistence.Configurations.IdentityModelconfiguration;


namespace Skinet.Persistence.Identity
{
    public class SkinetIdentityDbContext : IdentityDbContext<SkinetUser>
    {
        public SkinetIdentityDbContext(DbContextOptions<SkinetIdentityDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SkinetUserEntityConfiguration());
            builder.ApplyConfiguration(new SkinetUserAddressConfiguration());
            base.OnModelCreating(builder);
        }
    }
}

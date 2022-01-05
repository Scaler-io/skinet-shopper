using Microsoft.AspNetCore.Identity;

namespace Skinet.Entities.Entities.Identity
{
    public class SkinetUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}

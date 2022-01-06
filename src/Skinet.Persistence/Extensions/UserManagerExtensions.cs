using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skinet.Entities.Entities.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skinet.Persistence.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<SkinetUser> FindByEmailWithAddressAsync(this UserManager<SkinetUser>
            input, ClaimsPrincipal user)
        {
            var email = user?.FindFirst(ClaimTypes.Email).Value;

            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x =>x.Email == email);
        }

        public static async Task<SkinetUser> FindByEmailFromClaimsPrincipleAsync(this UserManager<SkinetUser> input,
            ClaimsPrincipal user)
        {
            var email = user?.FindFirst(ClaimTypes.Email).Value;
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}

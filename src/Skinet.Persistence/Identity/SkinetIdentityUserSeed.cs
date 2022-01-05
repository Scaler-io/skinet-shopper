using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Skinet.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Persistence.Identity
{
    public class SkinetIdentityUserSeed
    {
        public static async Task SeedAsync(UserManager<SkinetUser> userManager, ILogger<SkinetIdentityUserSeed> logger)
        {
            if (!userManager.Users.Any())
            {
                var users = GetPreconfiguredUsers(logger);
                foreach(var user in users)
                {
                    await userManager.CreateAsync(user, "P@ssw0rd");
                }
            }
        }

        private static IEnumerable<SkinetUser> GetPreconfiguredUsers(ILogger<SkinetIdentityUserSeed> logger)
        {
            try
            {
                var userData = File.ReadAllText("../Skinet.Persistence/SeedData/users.json");
                var users = JsonConvert.DeserializeObject<List<SkinetUser>>(userData);
                logger.LogInformation("users data loaded from users.json file");
                return users;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unable to load users data from users.json file");
            }

            return null;
        }
    }
}

using Skinet.Entities.Entities.Identity;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface ITokenService
    {
        string createToken(SkinetUser user);
    }
}

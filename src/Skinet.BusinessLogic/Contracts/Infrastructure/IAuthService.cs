using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> SigninAsync(SigninRequestDto request);
        Task<Result<AuthResponseDto>> SignupAsync(SignupRequestDto request);
        Task<Result<AuthResponseDto>> GetAuthUserAsync();
        Task<Result<bool>> CheckEmailExistsAsync(string email);
        Task<Result<UserAddressDto>> GetUserAddressAsync();
        Task<Result<UserAddressDto>> UpdateAddressAsync(UserAddressDto address);
    }
}

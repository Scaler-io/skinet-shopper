using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;
using Skinet.Entities.Entities.Identity;
using Skinet.Persistence.Extensions;
using Skinet.Shared.LoggerExtensions;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<SkinetUser> _userManager;
        private readonly SignInManager<SkinetUser> _signinManager;
        private readonly ILogger<AuthService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;


        public AuthService(UserManager<SkinetUser> userManager, SignInManager<SkinetUser> signinManager, ILogger<AuthService> logger, ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _logger = logger;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Result<bool>> CheckEmailExistsAsync(string email)
        {
            _logger.Here(nameof(AuthService), nameof(CheckEmailExistsAsync));
            var result = await _userManager.FindByEmailAsync(email) != null;
            _logger.Exited();
            return Result<bool>.Success(result);
        }

        public async Task<Result<AuthResponseDto>> GetAuthUserAsync()
        {
            _logger.Here(nameof(AuthService), nameof(GetAuthUserAsync));

            var user = await _userManager.FindByEmailFromClaimsPrincipleAsync(_httpContextAccessor.HttpContext.User);
                
            var response = new AuthResponseDto
            {
                Email = user.Email,
                DisplayName = user.Email,
                Token = _tokenService.createToken(user)
            };
            
            _logger.Exited();

            return Result<AuthResponseDto>.Success(response);
        }

        public async Task<Result<UserAddressDto>> GetUserAddressAsync()
        {
            _logger.Here(nameof(AuthService), nameof(GetAuthUserAsync));

            var user = await _userManager.FindByEmailWithAddressAsync(_httpContextAccessor.HttpContext.User);

            _logger.Exited();
            return Result<UserAddressDto>.Success(_mapper.Map<UserAddressDto>(user.Address));
        }

        public async Task<Result<AuthResponseDto>> SigninAsync(SigninRequestDto request)
        {
            _logger.Here(nameof(AuthService), nameof(SigninAsync));

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                _logger.LogError($"No user was found with email {request.Email}");
                return Result<AuthResponseDto>.Failure("Unauthorised");
            }

            var result = await _signinManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                _logger.LogError("attempt for signin was failed. credentials were invalid");
                return Result<AuthResponseDto>.Failure("Unauthorised");
            }


            _logger.LogInformation("signin was successfull");

            var response = new AuthResponseDto
            {
                Email = user.Email,
                DisplayName = user.Email,
                Token = _tokenService.createToken(user)
            };

            return Result<AuthResponseDto>.Success(response);
        }

        public async Task<Result<AuthResponseDto>> SignupAsync(SignupRequestDto request)
        {
            _logger.Here(nameof(AuthService), nameof(SignupAsync));

            var user = new SkinetUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                _logger.LogError($"User createion failed.");
                return Result<AuthResponseDto>.Failure($"Sign up was failed for the user email {request.Email}");
            }

            var response = new AuthResponseDto
            {
                Email = user.Email,
                DisplayName = user.Email,
                Token = _tokenService.createToken(user)
            };

            _logger.LogInformation($"Signup was successfull for the user {user.Email}");
            _logger.Exited();

            return Result<AuthResponseDto>.Success(response);
        }

        public async Task<Result<UserAddressDto>> UpdateAddressAsync(UserAddressDto address)
        {
            _logger.Here(nameof(AuthService), nameof(UpdateAddressAsync));

            var user = await _userManager.FindByEmailWithAddressAsync(_httpContextAccessor.HttpContext.User);

            user.Address = _mapper.Map<Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("user address update was failed. Something was wrong in the payload");
                return Result<UserAddressDto>.Failure("address update failed");
            }

            _logger.LogInformation("User address update successfull");
            _logger.Exited();

            return Result<UserAddressDto>.Success(_mapper.Map<UserAddressDto>(user.Address));
        }
    }
}

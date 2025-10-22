using IdentityService.Application.DTOs;
using IdentityService.Application.Common;

namespace IdentityService.Application.Services;

public interface IAuthService
{
    Task<Result<SignUpResponseDto>> SignUpAsync(SignUpRequestDto request);
}
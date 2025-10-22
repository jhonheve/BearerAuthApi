using IdentityService.Application.DTOs;
using IdentityService.Application.Common;
using IdentityService.Application.Services;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Repositories;
using IdentityService.Domain.Services;

namespace IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<SignUpResponseDto>> SignUpAsync(SignUpRequestDto request)
    {
        try
        {
            // Check if email already exists
            var emailExists = await _userRepository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                return Result<SignUpResponseDto>.Failure("Email already exists");
            }

            // Hash password
            var passwordHash = _passwordService.HashPassword(request.Password, out string salt);

            // Create user
            var user = new UserModel
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.ToLowerInvariant(),
                PasswordHash = passwordHash,
                Salt = salt,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var createdUser = await _userRepository.CreateAsync(user);

            // Map to response DTO
            var response = new SignUpResponseDto
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email,
                CreatedAt = createdUser.CreatedAt,
                IsActive = createdUser.IsActive
            };

            return Result<SignUpResponseDto>.Success(response, "User created successfully");
        }
        catch (Exception ex)
        {
            return Result<SignUpResponseDto>.Failure($"An error occurred during sign up: {ex.Message}");
        }
    }
}
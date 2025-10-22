using IdentityService.Application.DTOs;
using IdentityService.Application.Common;
using IdentityService.Domain.Repositories;
using IdentityService.Domain.Services;
using IdentityService.Domain.ValueObjects;
using IdentityService.Domain.Exceptions;
using IdentityService.Domain.Factories;

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
            var emailExists = await _userRepository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                return Result<SignUpResponseDto>.Failure("Email already exists");
            }

            var email = new Email(request.Email);
            var password = new Password(request.Password);

            // Create user using factory method (business rules enforced here)
            var user = UserFactory.Create(request.FirstName, request.LastName, email, password, _passwordService);
            var createdUser = await _userRepository.CreateAsync(user);

            // Map to response DTO, future mapping can be done with AutoMapper or similar
            var response = new SignUpResponseDto
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email.Value, // Convert value object to string
                CreatedAt = createdUser.CreatedAt,
                IsActive = createdUser.IsActive
            };

            return Result<SignUpResponseDto>.Success(response, "User created successfully");
        }
        catch (ArgumentException ex) // Domain validation failures (value objects)
        {
            return Result<SignUpResponseDto>.Failure(ex.Message);
        }
        catch (DomainException ex) // Business rule violations (entity)
        {
            return Result<SignUpResponseDto>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<SignUpResponseDto>.Failure($"An error occurred during sign up: {ex.Message}");
        }
    }
}
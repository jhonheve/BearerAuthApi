using IdentityService.Domain.Entities;
using IdentityService.Domain.ValueObjects;
using IdentityService.Domain.Services;
using IdentityService.Domain.Validators;

namespace IdentityService.Domain.Factories;

public static class UserFactory
{
    public static UserModel Create(string firstName, string lastName, Email email, Password password, IPasswordService passwordService)
    {
        // Validate input using domain validator
        UserValidator.ValidateUserCreation(firstName, lastName, email, password);
        var passwordHash = passwordService.HashPassword(password.Value, out string salt);
        return new(
            id: Guid.NewGuid(),
            firstName: firstName.Trim(),
            lastName: lastName.Trim(),
            email: email,
            passwordHash: passwordHash,
            salt: salt,
            createdAt: DateTime.UtcNow,
            isActive: true
        );
    }
}
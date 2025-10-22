using IdentityService.Domain.ValueObjects;
using IdentityService.Domain.Exceptions;

namespace IdentityService.Domain.Validators;

public static class UserValidator
{
    public static void ValidateUserCreation(string firstName, string lastName, Email email, Password password)
    {
        ValidateName(firstName, "First name");
        ValidateName(lastName, "Last name");

        if (email == null)
            throw new DomainException("Email cannot be null");

        if (password == null)
            throw new DomainException("Password cannot be null");
    }

    public static void ValidateProfileUpdate(string firstName, string lastName)
    {
        ValidateName(firstName, "First name");
        ValidateName(lastName, "Last name");
    }

    public static void ValidateName(string name, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException($"{fieldName} cannot be empty");

        if (name.Trim().Length < 2)
            throw new DomainException($"{fieldName} must be at least 2 characters");

        if (name.Trim().Length > 50)
            throw new DomainException($"{fieldName} cannot exceed 50 characters");
    }
}
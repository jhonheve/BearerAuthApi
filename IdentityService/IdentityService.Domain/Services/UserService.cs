using IdentityService.Domain.Entities;
using IdentityService.Domain.Validators;

namespace IdentityService.Domain.Services;

public static class UserService
{
    public static void UpdateProfile(UserModel user, string firstName, string lastName)
    {
        UserValidator.ValidateProfileUpdate(firstName, lastName);
        user.FirstName = firstName.Trim();
        user.LastName = lastName.Trim();
        user.UpdatedAt = DateTime.UtcNow;
    }

    public static void Deactivate(UserModel user)
    {
        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;
    }

    public static void Activate(UserModel user)
    {
        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;
    }
}
using IdentityService.Domain.Entities;
using IdentityService.Domain.Repositories;

namespace IdentityService.Infrastructure.Repositories;

/// <summary>
/// Provides an in-memory implementation of the IUserRepository interface for managing user data. Intended for scenarios
/// such as testing or prototyping where persistent storage is not required.
/// </summary>
/// <remarks>All user data is stored in memory and will be lost when the application is stopped or restarted. This
/// implementation is not thread-safe and should not be used in production environments. For persistent or concurrent
public class InMemoryUserRepository : IUserRepository
{
    private readonly List<UserModel> _users = [];

    public Task<UserModel?> GetByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(user);
    }

    public Task<UserModel?> GetByIdAsync(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<UserModel> CreateAsync(UserModel user)
    {
        _users.Add(user);
        return Task.FromResult(user);
    }

    public Task<UserModel> UpdateAsync(UserModel user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("User not found");
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.UpdatedAt = DateTime.UtcNow;
        existingUser.IsActive = user.IsActive;
        return Task.FromResult(existingUser);
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        var exists = _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exists);
    }
}
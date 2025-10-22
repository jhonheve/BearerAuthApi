using IdentityService.Domain.Entities;
using IdentityService.Domain.Repositories;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<UserModel> _users = new();

    public Task<UserModel?> GetByEmailAsync(Email email)
    {
        return GetByEmailAsync(email.Value);
    }

    public Task<UserModel?> GetByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email.Value.Equals(email, StringComparison.OrdinalIgnoreCase));
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
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id)
            ?? throw new InvalidOperationException("User not found");
        
        _users.Remove(existingUser);
        _users.Add(user);
        return Task.FromResult(user);
    }

    public Task<bool> EmailExistsAsync(Email email)
    {
        return EmailExistsAsync(email.Value);
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        var exists = _users.Any(u => u.Email.Value.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exists);
    }
}
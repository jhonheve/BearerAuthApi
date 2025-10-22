using IdentityService.Domain.Entities;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Domain.Repositories;

public interface IUserRepository
{
    Task<UserModel?> GetByEmailAsync(Email email);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<UserModel?> GetByIdAsync(Guid id);
    Task<UserModel> CreateAsync(UserModel user);
    Task<UserModel> UpdateAsync(UserModel user);
    Task<bool> EmailExistsAsync(Email email);
    Task<bool> EmailExistsAsync(string email);
}
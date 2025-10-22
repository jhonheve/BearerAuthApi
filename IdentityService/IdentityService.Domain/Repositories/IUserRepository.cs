using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Repositories;

public interface IUserRepository
{
    Task<UserModel?> GetByEmailAsync(string email);
    Task<UserModel?> GetByIdAsync(Guid id);
    Task<UserModel> CreateAsync(UserModel user);
    Task<UserModel> UpdateAsync(UserModel user);
    Task<bool> EmailExistsAsync(string email);
}
using IdentityService.Domain.Services;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 32; // 256 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 100000; // PBKDF2 iterations

    public string HashPassword(string password, out string salt)
    {
        // Generate a salt
        byte[] saltBytes = new byte[SaltSize];
        RandomNumberGenerator.Fill(saltBytes);
        salt = Convert.ToBase64String(saltBytes);

        // Hash the password
        byte[] hash = HashPasswordWithSalt(password, saltBytes);
        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        try
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashBytes = Convert.FromBase64String(hash);

            byte[] testHash = HashPasswordWithSalt(password, saltBytes);

            return CryptographicOperations.FixedTimeEquals(hashBytes, testHash);
        }
        catch
        {
            return false;
        }
    }

    private static byte[] HashPasswordWithSalt(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, Iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(HashSize);
    }
}

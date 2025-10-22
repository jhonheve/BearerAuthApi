using IdentityService.Domain.ValueObjects;

namespace IdentityService.Domain.Entities;

public class UserModel
{
    /// <summary>
    /// needs to be storedas strings in MongoDB, so I won't use [BsonId] or [BsonRepresentation] attributes here.
    /// </summary>
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public Email Email { get; set; } = null!; // Non-nullable, will be set via constructor

    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    // Parameterless constructor for MongoDB serialization
    public UserModel() { }


    // Constructor with parameters for easier object creation
    public UserModel(Guid id, string firstName, string lastName, Email email, string passwordHash, string salt, DateTime createdAt, bool isActive = true)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        CreatedAt = createdAt;
        IsActive = isActive;
    }
}
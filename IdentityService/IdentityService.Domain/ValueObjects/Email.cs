namespace IdentityService.Domain.ValueObjects;

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty");

        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email format");

        Value = value.ToLowerInvariant();
    }

    private static bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email,
          @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }


    public static implicit operator string(Email email) => email.Value;
    public override string ToString() => Value;
}
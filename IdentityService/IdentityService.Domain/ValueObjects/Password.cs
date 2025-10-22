namespace IdentityService.Domain.ValueObjects;

public record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be empty");

        // Business rule: Password complexity
        if (!MeetsComplexityRequirements(value))
            throw new ArgumentException("Password does not meet complexity requirements");

        Value = value;
    }

    private static bool MeetsComplexityRequirements(string password)
    {
        return password.Length >= 8 &&
           password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit) &&
               password.Any("@$!%*?&".Contains);
    }


    public static implicit operator string(Password password) => password.Value;
    public override string ToString() => Value;
}
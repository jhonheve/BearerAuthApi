# DDD Implementation - Identity Service

## Architecture Overview

This implementation follows **Domain-Driven Design (DDD)** principles with clean separation of concerns. The `User` entity is kept as a simple data model suitable for MongoDB document storage.

### Layers & Responsibilities

#### 1. **Domain Layer** (Business Logic)
- **Entities**: `User` - Simple data model suitable for MongoDB documents
- **Factories**: `UserFactory` - Handles user creation with business rules
- **Services**: `UserService` - User business operations (update, activate, deactivate)
- **Validators**: `UserValidator` - Domain validation logic
- **Value Objects**: `Email`, `Password` with validation
- **Services**: `IPasswordService` for domain services
- **Repositories**: `IUserRepository` interface
- **Exceptions**: `DomainException` for business rule violations

#### 2. **Application Layer** (Use Cases)
- **DTOs**: `SignUpRequestDto` (with data annotations), `SignUpResponseDto`
- **Services**: `AuthService` orchestrates domain operations
- **Results**: `Result<T>` for consistent response handling

#### 3. **Infrastructure Layer** (Technical Implementation)
- **Repositories**: `InMemoryUserRepository` implementation
- **Services**: `PasswordService` with PBKDF2 encryption

### Key DDD Features Implemented

#### ? **Clean Entity for MongoDB**
```csharp
// User entity as simple data model - perfect for MongoDB documents
public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Email Email { get; set; } = null!;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
```

#### ? **Factory Pattern for Creation**
```csharp
// UserFactory enforces business invariants during creation
var user = UserFactory.Create(firstName, lastName, email, password, passwordService);
```

#### ? **Service Pattern for Operations**
```csharp
// UserService handles business operations on existing users
UserService.UpdateProfile(user, newFirstName, newLastName);
UserService.Deactivate(user);
UserService.Activate(user);
```

#### ? **Value Objects with Domain Validation**
```csharp
// Email value object validates business rules
var email = new Email("user@example.com"); // Throws if invalid

// Password value object enforces complexity
var password = new Password("SecureP@ss123"); // Throws if weak
```

#### ? **Separated Validation Logic**
```csharp
// UserValidator contains all validation logic
UserValidator.ValidateUserCreation(firstName, lastName, email, password);
UserValidator.ValidateProfileUpdate(firstName, lastName);
```

#### ? **Layered Validation**
1. **Data Annotations** (Application): Format/input validation
2. **Value Objects** (Domain): Business format rules  
3. **Validators** (Domain): Business logic rules
4. **Factory/Service** (Domain): Business operation rules

#### ? **Domain Exceptions**
- `ArgumentException`: Value object validation failures
- `DomainException`: Business rule violations
- Both properly handled in Application layer

### Testing the Implementation

#### Valid Request:
```json
POST /auth-api/auth/signup
{
  "firstName": "John",
  "lastName": "Doe", 
  "email": "john.doe@example.com",
  "password": "SecureP@ssw0rd123"
}
```

#### Invalid Requests (Domain Validation):
```json
// Weak password (Domain validation)
{ "password": "123" } // ? "Password does not meet complexity requirements"

// Invalid email (Domain validation)  
{ "email": "invalid-email" } // ? "Invalid email format"

// Short name (Domain validation)
{ "firstName": "A" } // ? "First name must be at least 2 characters"
```

### Usage Examples

#### Creating a User:
```csharp
// Use UserFactory for creation
var email = new Email("user@example.com");
var password = new Password("SecureP@ss123");
var user = UserFactory.Create("John", "Doe", email, password, passwordService);
```

#### Updating User Profile:
```csharp
// Use UserService for operations
UserService.UpdateProfile(user, "Jane", "Smith");
```

#### Deactivating User:
```csharp
UserService.Deactivate(user);
```

### Benefits of This Architecture

1. **MongoDB Ready**: User entity is a simple POCO perfect for document storage
2. **Clean Separation**: Business logic separated from data model
3. **Testability**: Each component can be tested independently
4. **Maintainability**: Changes to business rules happen in specific places
5. **Consistency**: Domain rules enforced regardless of entry point
6. **Flexibility**: Easy to add new operations without modifying the User entity

### Domain Rules Enforced

- **Names**: 2-50 characters, not null/whitespace
- **Email**: Valid format, case-insensitive storage
- **Password**: 8+ chars, upper/lower/digit/special character
- **User Creation**: Immutable creation with proper validation
- **Email Uniqueness**: Enforced at repository level
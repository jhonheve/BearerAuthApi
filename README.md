# Bearer Authentication API

A comprehensive .NET 9 Web API solution for user authentication and identity management using Bearer token authentication. 
This project implements Domain-Driven Design (DDD) principles with Clean Architecture patterns.

## Features

- **User Registration (Sign Up)**: Complete user registration with validation
- **User Authentication (Sign In)**: Secure user login with Bearer token generation
- **Profile Management**: User profile updates and management
- **Bearer Token Authentication**: JWT-based authentication system
- **Domain-Driven Design**: Clean architecture with proper separation of concerns
- **Validation**: Comprehensive input validation with data annotations
- **Error Handling**: Robust error handling with custom result patterns

## Architecture

This solution follows Clean Architecture principles with Domain-Driven Design:

```
BearerAuthApi/
├── BearerAuth/ # Presentation Layer (Web API)
│   ├── Controllers/
│   │   └── AuthController.cs          # Authentication endpoints
│   ├── Program.cs             # Application configuration
│   └── BearerAuth.csproj
└── IdentityService/                 # Identity Bounded Context
    ├── IdentityService.Domain/          # Domain Layer
    │   ├── Entities/
    │   ├── ValueObjects/
    │   ├── Repositories/
    │   ├── Services/
    │   ├── Factories/
    │   ├── Validators/
    │   └── Exceptions/
    ├── IdentityService.Application/     # Application Layer
    │   ├── Services/
    │   ├── DTOs/
    │   └── Common/
└── IdentityService.Infrastructure/  # Infrastructure Layer
        ├── Repositories/
        └── Services/
```

## 🛠️ Technology Stack

- **.NET 9**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API framework
- **C# 13.0**: Latest C# language features
- **OpenAPI/Swagger**: API documentation
- **Domain-Driven Design**: Architecture pattern
- **Clean Architecture**: Layered architecture
- **Bearer Token Authentication**: JWT-based security

## 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or Visual Studio Code
- Git

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/jhonheve/BearerAuthApi.git
cd BearerAuthApi
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the Application

```bash
cd BearerAuth
dotnet run
```

The API will be available at:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`

### 5. Access API Documentation

When running in Local environment, navigate to:
- **OpenAPI/Swagger**: `https://localhost:5001/openapi/v1.json`

## 📚 API Endpoints

### Authentication Endpoints

#### POST `/auth-api/auth/signup`
Register a new user account.

**Request Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "SecurePass123!"
}
```

**Response (201 Created):**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "createdAt": "2024-01-15T10:30:00Z",
  "isActive": true
}
```

**Validation Rules:**
- **First Name**: Required, 2-50 characters
- **Last Name**: Required, 2-50 characters
- **Email**: Required, valid email format, max 256 characters
- **Password**: Required, 8-100 characters, must contain:
  - At least one uppercase letter
  - At least one lowercase letter
  - At least one number
  - At least one special character (@$!%*?&)

### Upcoming Endpoints

🚧 **Coming Soon:**
- `POST /auth-api/auth/signin` - User authentication
- `PUT /auth-api/auth/profile` - Update user profile
- `GET /auth-api/auth/profile` - Get user profile
- `POST /auth-api/auth/refresh` - Refresh JWT token
- `POST /auth-api/auth/signout` - User logout

## Domain Model

### User Entity
- **Properties**: Id, FirstName, LastName, Email, PasswordHash, CreatedAt, UpdatedAt, IsActive
- **Business Rules**: Email uniqueness, password complexity, user activation

### Value Objects
- **Email**: Validates email format and ensures immutability
- **Password**: Handles password validation and security requirements

### Domain Services
- **UserService**: Core user business logic
- **PasswordService**: Password hashing and validation
- **UserValidator**: User entity validation rules

## Configuration

### Environment Variables

The application supports configuration through environment variables with the prefix `ASPNETCORE_`:

```bash
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=https://localhost:5001;http://localhost:5000
```

### Configuration Sources

The application loads configuration from:
1. `appsettings.json`
2. `appsettings.{Environment}.json`
3. Environment variables (prefixed with `ASPNETCORE_`)
4. Command line arguments

## 🧪 Testing

### Run Unit Tests

```bash
dotnet test
```

### Integration Tests

```bash
dotnet test --filter Category=Integration
```

## 🚀 Deployment

### Local Development

```bash
dotnet run --environment Local
```

### Production Build

```bash
dotnet publish -c Release -o ./publish
```

## 📁 Project Structure

### BearerAuth (Presentation Layer)
- **Controllers**: API endpoints and HTTP request handling
- **Program.cs**: Application startup and dependency injection configuration

### IdentityService.Domain (Domain Layer)
- **Entities**: Core business entities (User)
- **Value Objects**: Immutable objects with business meaning (Email, Password)
- **Repositories**: Data access contracts
- **Services**: Domain services interfaces
- **Factories**: Object creation with business rules
- **Validators**: Domain validation logic
- **Exceptions**: Domain-specific exceptions

### IdentityService.Application (Application Layer)
- **Services**: Application services and use cases
- **DTOs**: Data transfer objects for API communication
- **Common**: Shared application components (Result pattern)

### IdentityService.Infrastructure (Infrastructure Layer)
- **Repositories**: Data access implementations
- **Services**: External service implementations

## 🔒 Security Features

- **Password Hashing**: Secure password storage using industry standards
- **Input Validation**: Comprehensive validation at multiple layers
- **Email Uniqueness**: Prevents duplicate user registrations
- **Domain Validation**: Business rule enforcement in the domain layer
- **Bearer Token Authentication**: JWT-based stateless authentication (planned)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- **Jhon Edison Henao Vega** - [GitHub Profile](https://github.com/jhonheve)

## 🙏 Acknowledgments

- Clean Architecture
- Domain-Driven Design 
- .NET Team for the excellent framework
- ASP.NET Core community for best practices

## 📞 Support

If you have any questions or need help with setup, please:
1. Check the [Issues](https://github.com/jhonheve/BearerAuthApi/issues) page
2. Create a new issue if your problem isn't already reported
3. Provide detailed information about your environment and the issue

---

**Built with ❤️ using .NET 9 and Clean Architecture principles**
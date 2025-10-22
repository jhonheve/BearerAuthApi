# Identity Service API

## Sign Up Endpoint

### POST `/auth-api/auth/signup`

Creates a new user account with encrypted password.

#### Request Body
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "SecureP@ssw0rd123"
}
```

#### Response (201 Created)
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

#### Response (400 Bad Request)
```json
{
  "message": "Email already exists",
  "errors": []
}
```

#### Password Requirements
- Minimum 8 characters
- At least one uppercase letter
- At least one lowercase letter
- At least one number
- At least one special character (@$!%*?&)

#### Features
- ? Password encryption using PBKDF2 with SHA256
- ? Salt generation for enhanced security
- ? Email uniqueness validation
- ? Input validation with data annotations
- ? Clean architecture implementation
- ? Proper error handling

#### Security Features
- Passwords are hashed using PBKDF2 with 100,000 iterations
- Each password gets a unique 256-bit salt
- Email addresses are stored in lowercase
- Constant-time comparison for password verification
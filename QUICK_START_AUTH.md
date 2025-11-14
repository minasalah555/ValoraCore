# ?? Valora Auth - Quick Start Guide

## ?? What's New

### ? Completed Implementation
1. **Auto-Admin on Login** - Username "admin" gets Admin role automatically
2. **Clean DTOs** - Secure, minimal, no sensitive data
3. **AutoMapper** - Clean entity ? DTO mapping
4. **JWT Service** - Dedicated token generation service
5. **Production-Ready** - Async, DI, logging, validation

---

## ?? Quick Test (30 seconds)

### 1. Start the API
```bash
cd Valora_WebApi
dotnet run
```

### 2. Test Auto-Admin Feature
Open Swagger: `https://localhost:5001/swagger`

**Login as Admin:**
```json
POST /api/Auth/login
{
  "userName": "admin",
  "password": "Admin@123"
}
```

**? Expected Result:**
```json
{
  "token": "eyJhbGc...",
  "tokenType": "Bearer",
  "expiresAt": "2024-11-20T10:30:00Z",
  "user": {
    "userName": "admin",
    "roles": ["Admin", "User"]  // ? Admin auto-assigned!
  }
}
```

### 3. Use Token
1. Copy the `token` value
2. Click **Authorize** button
3. Enter: `Bearer {your-token}`
4. Test admin endpoints!

---

## ?? API Endpoints Cheat Sheet

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/Auth/register` | No | Register new user |
| POST | `/api/Auth/login` | No | Login (auto-admin for "admin") |
| GET | `/api/Auth/me` | Yes | Get current user info |
| POST | `/api/Auth/logout` | Yes | Logout |

---

## ?? Code Examples

### Register New User
```csharp
POST /api/Auth/register
{
  "userName": "john_doe",
  "email": "john@example.com",
  "password": "SecurePass123",
  "confirmPassword": "SecurePass123"
}
```

### Login (Regular User)
```csharp
POST /api/Auth/login
{
  "userName": "john_doe",
  "password": "SecurePass123"
}
```

### Login (Auto-Admin)
```csharp
POST /api/Auth/login
{
  "userName": "admin",  // ? Magic happens here!
  "password": "Admin@123"
}
// Result: Admin role auto-assigned
```

### Use Token in Requests
```bash
curl -X GET https://localhost:5001/api/Auth/me \
  -H "Authorization: Bearer eyJhbGc..."
```

---

## ??? Project Structure

```
Valora_WebApi/
??? Controllers/
?   ??? AuthController.cs          ? Clean, rewritten
??? DTOs/
?   ??? Auth/                      ? NEW
?       ??? RegisterRequestDTO.cs
?       ??? LoginRequestDTO.cs
?       ??? AuthResponseDTO.cs
?       ??? UserDTO.cs             ? Secure, no sensitive fields
??? DTOs/AutoMapper/
?   ??? AuthProfile.cs             ? NEW - Auth mappings
??? Services/
?   ??? IJwtTokenService.cs        ? NEW - Interface
?   ??? JwtTokenService.cs         ? NEW - Token generation
??? Program.cs                     ? Updated - JWT service registered
```

---

## ?? Security Features

### ? What's Protected
- All sensitive fields hidden from DTOs
- Password requirements enforced
- JWT tokens signed and validated
- Role-based authorization ready
- Secure error messages

### ? What DTOs Include
```csharp
// ? UserDTO - Safe to expose
{
  "id": "abc123",
  "userName": "john_doe",
  "email": "john@example.com",
  "roles": ["User"]
}

// ? NOT included (kept internal):
// - passwordHash
// - securityStamp
// - concurrencyStamp
// - phoneNumberConfirmed
// - twoFactorEnabled
// - lockoutEnabled
```

---

## ?? Auto-Admin Logic

### How It Works
```csharp
// In AuthController.Login()
if (user.UserName?.Equals("admin", StringComparison.OrdinalIgnoreCase) == true)
{
    // 1. Ensure Admin role exists
    await EnsureRoleExistsAsync("Admin");
    
    // 2. Check if user already has Admin role
    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
    
    // 3. Assign if not already assigned
    if (!isAdmin)
    {
        await _userManager.AddToRoleAsync(user, "Admin");
        _logger.LogInformation("Admin role automatically assigned to user: {UserName}", user.UserName);
    }
}
```

### Why This Approach?
- ? No manual database changes needed
- ? No separate "Make Admin" endpoint
- ? Works on first login
- ? Idempotent (safe to repeat)
- ? Follows ASP.NET Identity best practices

---

## ?? Configuration

### JWT Settings (appsettings.json)
```json
{
  "Jwt": {
    "Key": "YourSecretKeyHere123456789012345678901234567890",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "ExpireDays": "7"
  }
}
```

### Services Registration (Program.cs)
```csharp
// AutoMapper - scans all profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// JWT Token Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { /* ... */ });
```

---

## ?? Testing Scenarios

### Scenario 1: First Admin Login
```bash
# Expected behavior:
1. Admin role is created (if doesn't exist)
2. Admin role is assigned to user "admin"
3. Token includes Admin role in claims
4. Log entry: "Admin role automatically assigned to user: admin"
```

### Scenario 2: Subsequent Admin Logins
```bash
# Expected behavior:
1. Admin role already exists (no creation)
2. User already has Admin role (no duplicate assignment)
3. Token includes Admin role in claims
4. No unnecessary database operations
```

### Scenario 3: Regular User Login
```bash
# Expected behavior:
1. No admin logic triggered
2. Token includes only User role
3. Normal authentication flow
```

### Scenario 4: Case-Insensitive Check
```bash
# All these usernames trigger auto-admin:
- "admin"
- "Admin"
- "ADMIN"
- "AdMiN"
```

---

## ?? Developer Notes

### Using AutoMapper
```csharp
// In AuthController
var userDto = _mapper.Map<UserDTO>(user);
userDto.Roles = await _userManager.GetRolesAsync(user);
```

### Generating Tokens
```csharp
// In AuthController
var token = await _jwtTokenService.GenerateTokenAsync(user);
var expiresAt = _jwtTokenService.GetTokenExpiration();
```

### Checking User Roles
```csharp
var roles = await _userManager.GetRolesAsync(user);
var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
```

### Protecting Endpoints
```csharp
[Authorize]                    // Requires authentication
[Authorize(Roles = "Admin")]   // Requires Admin role
[Authorize(Roles = "Admin,Manager")]  // Requires Admin OR Manager
```

---

## ?? Response Codes

| Code | Meaning | When |
|------|---------|------|
| 200 | Success | Login/Register successful |
| 400 | Bad Request | Invalid input, validation failed |
| 401 | Unauthorized | Invalid credentials, missing token |
| 404 | Not Found | User not found |

---

## ?? Troubleshooting

### Issue: "401 Unauthorized"
```bash
# Check:
1. Is token included? Authorization: Bearer {token}
2. Is token expired? Check expiresAt field
3. Is token format correct? Must start with "Bearer "
```

### Issue: "Admin role not assigned"
```bash
# Check:
1. Username is exactly "admin" (case-insensitive)
2. Check logs for: "Admin role automatically assigned"
3. Verify in database: AspNetUserRoles table
```

### Issue: "Invalid credentials"
```bash
# Check:
1. Username exists (register first)
2. Password is correct
3. Account is not locked out
```

---

## ?? Next Steps

### Optional Enhancements
1. Add refresh tokens
2. Implement password reset
3. Add email confirmation
4. Enable two-factor authentication
5. Add account lockout after failed attempts
6. Implement password history
7. Add social login (Google, Facebook)

### Testing
1. Write unit tests for JwtTokenService
2. Write integration tests for AuthController
3. Test role-based authorization
4. Test token expiration handling

---

## ?? Quick Commands

```bash
# Build project
dotnet build

# Run project
dotnet run

# Run with watch (auto-reload)
dotnet watch run

# Open Swagger
https://localhost:5001/swagger

# Check logs
# Look in console output for auto-admin messages
```

---

## ?? You're All Set!

Your authentication system is:
- ? Clean and minimal
- ? Secure and production-ready
- ? Auto-admin enabled
- ? Fully documented
- ? Easy to use and test

**Happy coding! ??**

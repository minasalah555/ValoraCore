# Valora Web API

A comprehensive RESTful API for an art e-commerce platform built with ASP.NET Core 8.0, Entity Framework Core, and JWT authentication.

## Features

- ? **Authentication & Authorization** - JWT-based authentication with role-based access control
- ? **Product Management** - Full CRUD operations for products with categories
- ? **Shopping Cart** - Add, update, remove items from cart
- ? **Order Processing** - Complete order management system
- ? **Product Reviews** - User reviews with ratings
- ? **AutoMapper** - DTO mapping for clean API responses
- ? **Swagger/OpenAPI** - Interactive API documentation
- ? **CORS** - Cross-Origin Resource Sharing enabled

## Tech Stack

- **Framework**: .NET 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity + JWT
- **Documentation**: Swagger/OpenAPI
- **Mapping**: AutoMapper

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Configuration

1. Update the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=ValoraDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
}
```

2. Update JWT settings if needed:
```json
"Jwt": {
  "Key": "YourSecretKeyHere123456789012345678901234567890",
  "Issuer": "ValoraAPI",
  "Audience": "ValoraClient",
  "ExpireDays": "7"
}
```

### Database Migration

Run the following commands to create the database:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Running the API

```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

### Default Admin Account

The system automatically seeds an admin account:
- **Username**: admin
- **Email**: admin@valora.com
- **Password**: Admin@123

## API Endpoints

### Authentication

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/Auth/Register` | Register new user | No |
| POST | `/api/Auth/Login` | Login user | No |
| POST | `/api/Auth/Logout` | Logout user | Yes |
| POST | `/api/Auth/AddRole` | Add role to user | Yes (Admin) |
| GET | `/api/Auth/User/{id}` | Get user details | Yes |

### Products

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Products` | Get all products | No |
| GET | `/api/Products/{id}` | Get product by ID | No |
| POST | `/api/Products` | Create product | Yes (Admin) |
| PUT | `/api/Products/{id}` | Update product | Yes (Admin) |
| DELETE | `/api/Products/{id}` | Delete product | Yes (Admin) |

### Categories

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Categories` | Get all categories | No |
| GET | `/api/Categories/{id}` | Get category by ID | No |
| GET | `/api/Categories/{id}/Products` | Get category with products | No |
| POST | `/api/Categories` | Create category | Yes (Admin) |
| PUT | `/api/Categories/{id}` | Update category | Yes (Admin) |
| DELETE | `/api/Categories/{id}` | Delete category | Yes (Admin) |

### Cart

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Cart/{cartId}` | Get cart by ID | Yes |
| GET | `/api/Cart/User/{userId}` | Get user's cart | Yes |
| GET | `/api/Cart/Count/{userId}` | Get cart item count | Yes |
| POST | `/api/Cart/AddItem` | Add item to cart | Yes |
| DELETE | `/api/Cart/RemoveItem/{cartId}/{productId}` | Remove item from cart | Yes |
| DELETE | `/api/Cart/{cartId}` | Delete entire cart | Yes |

### Orders

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Orders` | Get all orders | Yes (Admin) |
| GET | `/api/Orders/{orderId}` | Get order details | Yes |
| GET | `/api/Orders/User/{userId}` | Get user orders | Yes |
| GET | `/api/Orders/{orderId}/Total` | Get order total | Yes |
| POST | `/api/Orders` | Create order | Yes |
| PUT | `/api/Orders/UpdateStatus` | Update order status | Yes (Admin) |
| PUT | `/api/Orders/{orderId}/Cancel` | Cancel order | Yes |

### Reviews

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Reviews/Product/{productId}` | Get product reviews | No |
| GET | `/api/Reviews/User/{userId}` | Get user reviews | Yes |
| GET | `/api/Reviews/{reviewId}` | Get review details | No |
| GET | `/api/Reviews/Product/{productId}/Rating` | Get product rating | No |
| POST | `/api/Reviews` | Create review | Yes |
| PUT | `/api/Reviews` | Update review | Yes |
| DELETE | `/api/Reviews/{reviewId}` | Delete review | Yes |

## Request/Response Examples

### Register User

**Request:**
```json
POST /api/Auth/Register
{
  "userName": "john_doe",
  "email": "john@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!"
}
```

**Response:**
```json
{
  "message": "User registered successfully",
  "userId": "abc123...",
  "userName": "john_doe",
  "email": "john@example.com"
}
```

### Login

**Request:**
```json
POST /api/Auth/Login
{
  "userName": "john_doe",
  "password": "Password123!",
  "rememberMe": false
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": "abc123...",
  "userName": "john_doe",
  "email": "john@example.com",
  "roles": ["User"]
}
```

### Create Product (Admin)

**Request:**
```json
POST /api/Products
Authorization: Bearer {token}
{
  "name": "Abstract Canvas",
  "description": "Modern abstract art piece",
  "price": 299,
  "stockQuantity": 10,
  "imgUrl": "https://example.com/image.jpg",
  "categoryId": 1
}
```

### Add to Cart

**Request:**
```json
POST /api/Cart/AddItem
Authorization: Bearer {token}
{
  "userId": "abc123...",
  "cartId": 0,
  "productId": 5,
  "quantity": 2
}
```

### Create Order

**Request:**
```json
POST /api/Orders
Authorization: Bearer {token}
{
  "userId": "abc123...",
  "cartId": 1,
  "shippingAddress": "123 Main St",
  "city": "New York",
  "postalCode": "10001",
  "country": "USA",
  "phoneNumber": "+1234567890",
  "notes": "Ring doorbell"
}
```

### Create Review

**Request:**
```json
POST /api/Reviews
Authorization: Bearer {token}
{
  "productId": 5,
  "userId": "abc123...",
  "rating": 5,
  "title": "Excellent Quality",
  "comment": "Beautiful artwork, well packaged and shipped quickly."
}
```

## Authentication

The API uses JWT (JSON Web Tokens) for authentication. Include the token in the Authorization header:

```
Authorization: Bearer {your_token_here}
```

### Using Swagger UI

1. Click the "Authorize" button in Swagger UI
2. Enter: `Bearer {your_token}` (replace with actual token from login)
3. Click "Authorize"
4. You can now test protected endpoints

## Database Schema

### Main Entities

- **ApplicationUser** - Identity user with authentication
- **ApplicationRole** - Identity roles (Admin, User, Manager)
- **Product** - Product catalog
- **Category** - Product categories
- **Cart** - Shopping cart
- **CartItem** - Items in cart
- **Order** - Customer orders
- **OrderItem** - Items in order
- **Review** - Product reviews

## Project Structure

```
Valora_WebApi/
??? Controllers/          # API Controllers
?   ??? AuthController.cs
?   ??? ProductsController.cs
?   ??? CategoriesController.cs
?   ??? CartController.cs
?   ??? OrdersController.cs
?   ??? ReviewsController.cs
??? Models/              # Domain models
??? DTOs/                # Data Transfer Objects
?   ??? AutoMapper/      # AutoMapper profiles
??? ViewModels/          # View models for requests
??? Services/            # Business logic
??? Repositories/        # Data access layer
??? Data/                # DbContext
??? Program.cs           # Application entry point
```

## Error Handling

The API returns standard HTTP status codes:

- `200 OK` - Successful request
- `201 Created` - Resource created successfully
- `204 No Content` - Successful request with no content
- `400 Bad Request` - Invalid request data
- `401 Unauthorized` - Authentication required
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

## Roles & Permissions

### Admin
- Full access to all endpoints
- Can create, update, delete products and categories
- Can view all orders
- Can update order status

### User
- Can register and login
- Can browse products and categories
- Can manage their own cart
- Can place and view their own orders
- Can create and manage their own reviews

### Manager
- Can view orders
- Can update order status
- Cannot modify products or categories

## Development

### Adding New Migration

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Seeded Data

The application automatically seeds:
- 3 Roles (Admin, User, Manager)
- 1 Admin user
- 10 Categories (Oil Paintings, Sculptures, etc.)
- 100 Products across all categories

## License

This project is licensed under the MIT License.

## Support

For issues and questions, please contact the development team or create an issue in the repository.

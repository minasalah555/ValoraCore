# Valora Web API - Quick Start Guide

## ?? Quick Start (5 Minutes)

### Step 1: Update Database Connection (30 seconds)

Open `appsettings.json` and verify your SQL Server connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=ValoraDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
}
```

Change `Data Source=.` to your SQL Server instance name if needed.

### Step 2: Create Database (1 minute)

Open terminal in project directory and run:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Step 3: Run the Application (30 seconds)

```bash
dotnet run
```

Wait for the message: "Now listening on: https://localhost:5001"

### Step 4: Open Swagger UI (30 seconds)

Open your browser and navigate to:
```
https://localhost:5001/swagger
```

### Step 5: Test the API (2 minutes)

#### Login as Admin

1. Find the **POST /api/Auth/Login** endpoint
2. Click "Try it out"
3. Use these credentials:
```json
{
  "userName": "admin",
  "password": "Admin@123",
  "rememberMe": false
}
```
4. Click "Execute"
5. Copy the `token` from the response

#### Authorize Swagger

1. Click the **"Authorize"** button (top right with a lock icon)
2. Enter: `Bearer {paste_your_token_here}`
3. Click "Authorize"
4. Click "Close"

#### Test Endpoints

Now you can test any endpoint! Try these:

**Get All Products:**
- Endpoint: GET /api/Products
- No authentication needed
- Shows 100 seeded products

**Get All Categories:**
- Endpoint: GET /api/Categories
- No authentication needed
- Shows 10 categories

**Create a New Product (Admin only):**
- Endpoint: POST /api/Products
- Requires authentication
```json
{
  "name": "My Custom Artwork",
  "description": "Beautiful handmade piece",
  "price": 500,
  "stockQuantity": 5,
  "imgUrl": "https://picsum.photos/600/400",
  "categoryId": 1
}
```

## ?? Common Tasks

### Register a New User

Endpoint: POST /api/Auth/Register
```json
{
  "userName": "testuser",
  "email": "test@example.com",
  "password": "Test123!",
  "confirmPassword": "Test123!"
}
```

### Add Item to Cart

1. First, get your userId from login response
2. Endpoint: POST /api/Cart/AddItem
```json
{
  "userId": "your-user-id",
  "cartId": 0,
  "productId": 1,
  "quantity": 2
}
```

### Create an Order

Endpoint: POST /api/Orders
```json
{
  "userId": "your-user-id",
  "cartId": 1,
  "shippingAddress": "123 Main St",
  "city": "New York",
  "postalCode": "10001",
  "country": "USA",
  "phoneNumber": "+1234567890",
  "notes": "Please ring doorbell"
}
```

### Add a Product Review

Endpoint: POST /api/Reviews
```json
{
  "productId": 1,
  "userId": "your-user-id",
  "rating": 5,
  "title": "Excellent!",
  "comment": "Beautiful artwork, highly recommend!"
}
```

## ?? Default Accounts

### Admin Account
- **Username:** admin
- **Email:** admin@valora.com
- **Password:** Admin@123
- **Role:** Admin

Use this account to:
- Create/Update/Delete products
- Create/Update/Delete categories
- View all orders
- Update order status

## ?? Seeded Data

The database comes pre-loaded with:

- **100 Products** across 10 categories
- **10 Categories:** Oil Paintings, Acrylic, Watercolor, Sketches, Digital Prints, Wood Sculptures, Stone Sculptures, Metal Sculptures, Clay & Ceramic, Miniatures
- **3 Roles:** Admin, User, Manager

## ??? Troubleshooting

### Database Connection Issues

**Error:** Cannot connect to SQL Server

**Solution:**
1. Check SQL Server is running
2. Verify connection string in `appsettings.json`
3. Try: `Data Source=(localdb)\\MSSQLLocalDB` for LocalDB

### Migration Issues

**Error:** Migration already exists

**Solution:**
```bash
dotnet ef database drop --force
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### JWT Token Expired

**Solution:**
1. Login again to get a new token
2. Update the authorization in Swagger

### Port Already in Use

**Solution:**
Change the port in `Properties/launchSettings.json` or:
```bash
dotnet run --urls "https://localhost:5002"
```

## ?? API Endpoints Overview

| Category | Public | Authenticated | Admin Only |
|----------|--------|---------------|------------|
| **Auth** | 2 | 2 | 1 |
| **Products** | 2 | 0 | 3 |
| **Categories** | 3 | 0 | 3 |
| **Cart** | 0 | 6 | 0 |
| **Orders** | 0 | 5 | 2 |
| **Reviews** | 3 | 4 | 0 |

## ?? Example Workflow

### Complete Shopping Experience

1. **Browse Products** (No auth needed)
   - GET /api/Products
   - GET /api/Categories

2. **Register Account**
   - POST /api/Auth/Register

3. **Login**
   - POST /api/Auth/Login
   - Save the JWT token

4. **Add to Cart**
   - POST /api/Cart/AddItem

5. **View Cart**
   - GET /api/Cart/User/{userId}

6. **Create Order**
   - POST /api/Orders

7. **View Order**
   - GET /api/Orders/{orderId}

8. **Write Review**
   - POST /api/Reviews

## ?? Tips

1. **Use Swagger UI** for easy testing - it handles authentication automatically
2. **Keep your JWT token** handy - it expires after 7 days by default
3. **Check response status codes** - they indicate success or error type
4. **Use the admin account** to test all features
5. **Copy-paste example JSON** from this guide for quick testing

## ?? You're Ready!

Your Valora Web API is now fully functional. You can:
- ? Authenticate users
- ? Manage products and categories
- ? Handle shopping carts
- ? Process orders
- ? Collect product reviews

Start building your frontend or mobile app to consume this API!

## ?? Need Help?

- Check `README.md` for detailed documentation
- Review `PROJECT_SUMMARY.md` for technical details
- Use Swagger UI to explore all endpoints interactively

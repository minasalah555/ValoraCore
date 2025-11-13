# ?? Valora API - Quick Reference Card

## ?? Get Started in 3 Steps

### 1?? Make Admin
```bash
POST /api/Auth/MakeAdmin
{"userName": "your_username"}
```

### 2?? Login
```bash
POST /api/Auth/Login
{"userName": "your_username", "password": "your_password", "rememberMe": false}
```

### 3?? Copy Token
```json
{
  "token": "eyJhbG..." // ? Copy this
}
```

## ?? Most Used Endpoints

### ??? Shopping Flow
```bash
# Browse
GET /api/Products
GET /api/Categories

# Add to Cart
POST /api/Cart/AddItem
{"productId": 5, "quantity": 2}

# View Cart
GET /api/Cart/MyCart

# Checkout
POST /api/Orders
{shipping address details...}

# View Orders
GET /api/Orders/MyOrders
```

### ? Review Flow
```bash
# Leave Review
POST /api/Reviews
{"productId": 5, "rating": 5, "title": "Great!", "comment": "Love it!"}

# View My Reviews
GET /api/Reviews/MyReviews
```

### ????? Admin Tasks
```bash
# Create Product
POST /api/Products
{product details...}

# Update Order Status
PUT /api/Orders/{orderId}/Status
{"orderStatus": "Shipped"}

# View All Orders
GET /api/Orders
```

## ?? Key Endpoints at a Glance

| Action | Method | Endpoint | Auth |
|--------|--------|----------|------|
| **Get my cart** | GET | `/api/Cart/MyCart` | ? |
| **Get my orders** | GET | `/api/Orders/MyOrders` | ? |
| **Get my reviews** | GET | `/api/Reviews/MyReviews` | ? |
| **Add to cart** | POST | `/api/Cart/AddItem` | ? |
| **Remove from cart** | DELETE | `/api/Cart/RemoveItem/{productId}` | ? |
| **Clear cart** | DELETE | `/api/Cart/Clear` | ? |
| **Create order** | POST | `/api/Orders` | ? |
| **Cancel order** | PUT | `/api/Orders/{orderId}/Cancel` | ? |
| **Leave review** | POST | `/api/Reviews` | ? |
| **Update review** | PUT | `/api/Reviews/{reviewId}` | ? |

## ?? Authorization Header

For all protected endpoints:
```
Authorization: Bearer {your_token_here}
```

## ?? Pro Tips

1. **No User IDs Needed**: All "My*" endpoints auto-use your JWT token
2. **Clean Bodies**: Only provide necessary fields
3. **IDs in URLs**: For updates, ID goes in URL not body
4. **Cart Auto-Create**: Don't have a cart? It creates one automatically
5. **Login After Role Change**: Get new token with updated roles

## ?? Endpoint Count

- **Total**: 41 endpoints
- **Public**: 12 endpoints
- **User**: 19 endpoints
- **Admin**: 10 endpoints

## ?? Clean Request Examples

### ? Clean (After Improvements)
```json
POST /api/Cart/AddItem
Authorization: Bearer {token}
{
  "productId": 5,
  "quantity": 2
}
```

### ? Old Way (Before)
```json
POST /api/Cart/AddItem
{
  "userId": "abc123...",
  "cartId": 1,
  "productId": 5,
  "quantity": 2
}
```

## ?? Common Issues

### "Unauthorized"
? Did you include `Authorization: Bearer {token}` header?

### "Forbidden"
? Need Admin role? Use `/api/Auth/MakeAdmin` first

### Token Expired
? Login again to get new token

### Role Not Showing
? Login again after role change to get new token

## ?? Swagger UI

Navigate to: `https://localhost:5001/swagger`

1. Click "Authorize" ??
2. Enter: `Bearer {your_token}`
3. Click "Authorize"
4. Test all endpoints!

## ? Quick Testing Script

```bash
# 1. Make admin
POST /api/Auth/MakeAdmin {"userName": "test"}

# 2. Login
POST /api/Auth/Login {"userName": "test", "password": "Test123!", "rememberMe": false}

# 3. Add to cart
POST /api/Cart/AddItem {"productId": 1, "quantity": 2}

# 4. View cart
GET /api/Cart/MyCart

# 5. Create order
POST /api/Orders {address: "123 Main St", city: "NY", ...}

# 6. View orders
GET /api/Orders/MyOrders

# 7. Leave review
POST /api/Reviews {"productId": 1, "rating": 5, "title": "Great!"}
```

## ?? You're Ready!

**Full Docs**: Check `API_TESTING_GUIDE.md`

**Swagger**: `https://localhost:5001/swagger`

**Happy Coding!** ??

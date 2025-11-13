# Valora API - Complete Testing Guide

## ?? Quick Start - Make Yourself Admin

### Step 1: Register a New User
```json
POST /api/Auth/Register
Content-Type: application/json

{
  "userName": "testuser",
  "email": "test@example.com",
  "password": "Test123!",
  "confirmPassword": "Test123!"
}
```

### Step 2: Make Yourself Admin (Development Only)
```json
POST /api/Auth/MakeAdmin
Content-Type: application/json

{
  "userName": "testuser"
}
```

**Response:**
```json
{
  "message": "User 'testuser' is now an Admin. Please login again to get new JWT token with Admin role.",
  "userId": "abc123...",
  "userName": "testuser",
  "email": "test@example.com"
}
```

### Step 3: Login to Get JWT Token with Admin Role
```json
POST /api/Auth/Login
Content-Type: application/json

{
  "userName": "testuser",
  "password": "Test123!",
  "rememberMe": false
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": "abc123...",
  "userName": "testuser",
  "email": "test@example.com",
  "roles": ["User", "Admin"]
}
```

**? Now you have Admin access! Copy the token for use in other requests.**

---

## ?? All API Endpoints - Clean & Organized

### ?? Authentication Endpoints

#### 1. Register User
```http
POST /api/Auth/Register
```
**Body:**
```json
{
  "userName": "john_doe",
  "email": "john@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!"
}
```

#### 2. Login
```http
POST /api/Auth/Login
```
**Body:**
```json
{
  "userName": "john_doe",
  "password": "Password123!",
  "rememberMe": false
}
```

#### 3. Logout (Requires Auth)
```http
POST /api/Auth/Logout
Authorization: Bearer {token}
```

#### 4. Make Admin (Development - No Auth Required)
```http
POST /api/Auth/MakeAdmin
```
**Body:**
```json
{
  "userName": "john_doe"
}
```

#### 5. Request Role (Requires Auth - Self-Service)
```http
POST /api/Auth/RequestRole
Authorization: Bearer {token}
```
**Body:**
```json
{
  "roleName": "Manager"
}
```
**Note:** Cannot self-assign "Admin" role

#### 6. Add Role to User (Admin Only)
```http
POST /api/Auth/AddRole
Authorization: Bearer {admin_token}
```
**Body:**
```json
{
  "userId": "user-id-here",
  "roleName": "Manager"
}
```

#### 7. Get User Details (Requires Auth)
```http
GET /api/Auth/User/{userId}
Authorization: Bearer {token}
```

---

### ??? Product Endpoints

#### 1. Get All Products (Public)
```http
GET /api/Products
```

#### 2. Get Product by ID (Public)
```http
GET /api/Products/{id}
```

#### 3. Create Product (Admin Only)
```http
POST /api/Products
Authorization: Bearer {admin_token}
```
**Body:**
```json
{
  "name": "Abstract Canvas Art",
  "description": "Beautiful modern abstract painting",
  "price": 450,
  "stockQuantity": 10,
  "imgUrl": "https://picsum.photos/600/400",
  "categoryId": 1
}
```

#### 4. Update Product (Admin Only)
```http
PUT /api/Products/{id}
Authorization: Bearer {admin_token}
```
**Body:**
```json
{
  "name": "Updated Product Name",
  "description": "Updated description",
  "price": 500,
  "stockQuantity": 15,
  "imgUrl": "https://picsum.photos/600/400",
  "categoryId": 1
}
```

#### 5. Delete Product (Admin Only)
```http
DELETE /api/Products/{id}
Authorization: Bearer {admin_token}
```

---

### ?? Category Endpoints

#### 1. Get All Categories (Public)
```http
GET /api/Categories
```

#### 2. Get Category by ID (Public)
```http
GET /api/Categories/{id}
```

#### 3. Get Category with Products (Public)
```http
GET /api/Categories/{id}/Products
```

#### 4. Create Category (Admin Only)
```http
POST /api/Categories
Authorization: Bearer {admin_token}
```
**Body:**
```json
{
  "name": "Abstract Art",
  "description": "Modern abstract artworks"
}
```

#### 5. Update Category (Admin Only)
```http
PUT /api/Categories/{id}
Authorization: Bearer {admin_token}
```
**Body:**
```json
{
  "name": "Updated Category",
  "description": "Updated description"
}
```

#### 6. Delete Category (Admin Only)
```http
DELETE /api/Categories/{id}
Authorization: Bearer {admin_token}
```

---

### ?? Cart Endpoints (All Require Auth)

#### 1. Get My Cart
```http
GET /api/Cart/MyCart
Authorization: Bearer {token}
```
**? Automatically uses your user ID from JWT token**

#### 2. Get My Cart Item Count
```http
GET /api/Cart/Count
Authorization: Bearer {token}
```
**? Returns count of items in your cart**

#### 3. Get Cart by ID
```http
GET /api/Cart/{cartId}
Authorization: Bearer {token}
```

#### 4. Get User's Cart (Admin Only)
```http
GET /api/Cart/User/{userId}
Authorization: Bearer {admin_token}
```

#### 5. Add Item to Cart
```http
POST /api/Cart/AddItem
Authorization: Bearer {token}
```
**Body:**
```json
{
  "productId": 5,
  "quantity": 2,
  "cartId": null
}
```
**? User ID automatically extracted from JWT token**
**? If cartId is null, creates new cart automatically**

#### 6. Remove Item from Cart
```http
DELETE /api/Cart/RemoveItem/{productId}
Authorization: Bearer {token}
```
**? Automatically finds your cart and removes the product**

#### 7. Clear My Cart
```http
DELETE /api/Cart/Clear
Authorization: Bearer {token}
```
**? Deletes all items from your cart**

---

### ?? Order Endpoints (All Require Auth)

#### 1. Get All Orders (Admin Only)
```http
GET /api/Orders
Authorization: Bearer {admin_token}
```

#### 2. Get My Orders
```http
GET /api/Orders/MyOrders
Authorization: Bearer {token}
```
**? Automatically shows your orders using JWT token**

#### 3. Get Order by ID
```http
GET /api/Orders/{orderId}
Authorization: Bearer {token}
```

#### 4. Get User's Orders (Admin Only)
```http
GET /api/Orders/User/{userId}
Authorization: Bearer {admin_token}
```

#### 5. Get Order Total
```http
GET /api/Orders/{orderId}/Total
Authorization: Bearer {token}
```

#### 6. Create Order
```http
POST /api/Orders
Authorization: Bearer {token}
```
**Body:**
```json
{
  "shippingAddress": "123 Main Street",
  "city": "New York",
  "postalCode": "10001",
  "country": "USA",
  "phoneNumber": "+1234567890",
  "notes": "Ring doorbell",
  "cartId": null
}
```
**? User ID automatically extracted from JWT token**
**? If cartId is null, uses your current cart**

#### 7. Update Order Status (Admin Only)
```http
PUT /api/Orders/{orderId}/Status
Authorization: Bearer {admin_token}
```
**Body:**
```json
{
  "orderStatus": "Shipped",
  "shippedDate": "2025-01-15T10:00:00Z",
  "deliveredDate": null,
  "notes": "Shipped via FedEx"
}
```
**? Order ID taken from URL, not body**

#### 8. Cancel Order
```http
PUT /api/Orders/{orderId}/Cancel
Authorization: Bearer {token}
```

---

### ? Review Endpoints

#### 1. Get Product Reviews (Public)
```http
GET /api/Reviews/Product/{productId}
```

#### 2. Get My Reviews (Requires Auth)
```http
GET /api/Reviews/MyReviews
Authorization: Bearer {token}
```
**? Automatically shows your reviews using JWT token**

#### 3. Get User's Reviews (Public)
```http
GET /api/Reviews/User/{userId}
```

#### 4. Get Review by ID (Public)
```http
GET /api/Reviews/{reviewId}
```

#### 5. Get Product Rating (Public)
```http
GET /api/Reviews/Product/{productId}/Rating
```
**Returns average rating and review count**

#### 6. Create Review (Requires Auth)
```http
POST /api/Reviews
Authorization: Bearer {token}
```
**Body:**
```json
{
  "productId": 5,
  "rating": 5,
  "title": "Excellent Quality",
  "comment": "Beautiful artwork, fast shipping!"
}
```
**? User ID automatically extracted from JWT token**

#### 7. Update Review (Requires Auth)
```http
PUT /api/Reviews/{reviewId}
Authorization: Bearer {token}
```
**Body:**
```json
{
  "rating": 4,
  "title": "Good Quality",
  "comment": "Updated my review after more use"
}
```
**? Review ID taken from URL, not body**

#### 8. Delete Review (Requires Auth)
```http
DELETE /api/Reviews/{reviewId}
Authorization: Bearer {token}
```

---

## ?? Key Improvements - Cleaner ViewModels

### ? Before vs After

#### ? Before (Required unnecessary data):
```json
POST /api/Cart/AddItem
{
  "userId": "abc123...",  // ? Had to provide manually
  "cartId": 1,
  "productId": 5,
  "quantity": 2
}
```

#### ? After (Clean & secure):
```json
POST /api/Cart/AddItem
Authorization: Bearer {token}
{
  "productId": 5,         // ? Only what's needed!
  "quantity": 2,
  "cartId": null          // ? Optional
}
```
**? User ID extracted from JWT token automatically!**

---

## ?? Security Features

1. **JWT Token Auto-Extract**: User ID automatically extracted from token
2. **Route Parameter Priority**: IDs from URL override body values
3. **Admin Protection**: Cannot self-assign Admin role
4. **Token Validation**: All protected endpoints validate JWT
5. **Role-Based Access**: Admin, User, Manager roles enforced

---

## ?? Complete Endpoint Summary

| Category | Public | Authenticated | Admin Only | Total |
|----------|--------|---------------|------------|-------|
| **Auth** | 2 | 3 | 2 | 7 |
| **Products** | 2 | 0 | 3 | 5 |
| **Categories** | 3 | 0 | 3 | 6 |
| **Cart** | 0 | 7 | 0 | 7 |
| **Orders** | 0 | 6 | 2 | 8 |
| **Reviews** | 5 | 3 | 0 | 8 |
| **TOTAL** | **12** | **19** | **10** | **41** |

---

## ?? Testing Workflow

### 1. Setup
```bash
# Make yourself admin
POST /api/Auth/MakeAdmin {"userName": "your_username"}

# Login to get admin token
POST /api/Auth/Login
```

### 2. Browse Products
```bash
GET /api/Products
GET /api/Categories
```

### 3. Add to Cart
```bash
POST /api/Cart/AddItem {"productId": 1, "quantity": 2}
GET /api/Cart/MyCart
```

### 4. Create Order
```bash
POST /api/Orders {shipping details...}
GET /api/Orders/MyOrders
```

### 5. Leave Review
```bash
POST /api/Reviews {"productId": 1, "rating": 5, ...}
GET /api/Reviews/MyReviews
```

---

## ?? Important Notes

1. **MakeAdmin Endpoint**: Remove or secure in production
2. **JWT Tokens**: Expire after 7 days (configurable)
3. **Always Login Again**: After role changes to get new token
4. **ViewModels**: Only include necessary fields
5. **Route Parameters**: Used for IDs instead of body when possible

---

## ?? All Done!

Your API is now **clean, secure, and production-ready** with properly structured ViewModels and automatic user context from JWT tokens!

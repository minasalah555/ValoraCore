using Microsoft.EntityFrameworkCore;
using Valora.Data;
using Valora.DTOs;
using Valora.Models;

namespace Valora.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly Context _context;
        private readonly ICartRepository _cartRepository;

        public OrderRepository(Context context, ICartRepository cartRepository) : base(context)
        {
            _context = context;
            _cartRepository = cartRepository;
        }

        public async Task<OrderDTO?> GetOrderWithDetails(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.ID == orderId && !o.IsDeleted);

            if (order == null)
                return null;

            return new OrderDTO
            {
                OrderId = order.ID,
                UserId = order.UserID,
                UserName = order.User?.UserName ?? "Unknown",
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                ShippingAddress = order.ShippingAddress,
                City = order.City,
                PostalCode = order.PostalCode,
                Country = order.Country,
                PhoneNumber = order.PhoneNumber,
                ShippedDate = order.ShippedDate,
                DeliveredDate = order.DeliveredDate,
                Notes = order.Notes,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDTO
                {
                    OrderItemId = oi.ID,
                    OrderId = oi.OrderID,
                    ProductId = oi.ProductID,
                    ProductName = oi.Product?.Name ?? "Product",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList() ?? new List<OrderItemDTO>()
            };
        }

        public async Task<List<OrderDTO>> GetOrdersByUserId(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserID == userId && !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(order => new OrderDTO
            {
                OrderId = order.ID,
                UserId = order.UserID,
                UserName = order.User?.UserName ?? "Unknown",
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                ShippingAddress = order.ShippingAddress,
                City = order.City,
                PostalCode = order.PostalCode,
                Country = order.Country,
                PhoneNumber = order.PhoneNumber,
                ShippedDate = order.ShippedDate,
                DeliveredDate = order.DeliveredDate,
                Notes = order.Notes,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDTO
                {
                    OrderItemId = oi.ID,
                    OrderId = oi.OrderID,
                    ProductId = oi.ProductID,
                    ProductName = oi.Product?.Name ?? "Product",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList() ?? new List<OrderItemDTO>()
            }).ToList();
        }

        public async Task<Order> CreateOrderFromCart(string userId, int cartId, string shippingAddress, string city, string postalCode, string country, string phoneNumber, string? notes)
        {
            var cart = await _context.Carts
                .AsTracking()
                .Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.ID == cartId && c.UserID == userId && !c.IsDeleted);

            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart not found or is empty");
            }

            // Create order
            var order = new Order
            {
                UserID = userId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Pending",
                ShippingAddress = shippingAddress,
                City = city,
                PostalCode = postalCode,
                Country = country,
                PhoneNumber = phoneNumber,
                Notes = notes,
                TotalAmount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Create order items from cart items
            decimal totalAmount = 0;
            foreach (var cartItem in cart.CartItems)
            {
                // Product.Price is int; convert to decimal to avoid reflection and type issues
                var unitPrice = cartItem.Product != null ? (decimal)cartItem.Product.Price : 0m;
                var totalPrice = unitPrice * cartItem.Quantity;

                var orderItem = new OrderItem
                {
                    OrderID = order.ID,
                    ProductID = cartItem.ProductID,
                    Quantity = cartItem.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = totalPrice,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _context.OrderItems.AddAsync(orderItem);
                totalAmount += totalPrice;
            }

            // Update order total amount
            order.TotalAmount = totalAmount;
            order.UpdatedAt = DateTime.UtcNow;
            _context.Orders.Update(order);

            // Clear cart items
            foreach (var cartItem in cart.CartItems)
            {
                cartItem.IsDeleted = true;
                cartItem.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status, DateTime? shippedDate, DateTime? deliveredDate, string? notes)
        {
            var order = await GetByIDWithTracking(orderId);
            if (order == null)
                return false;

            order.OrderStatus = status;
            order.ShippedDate = shippedDate;
            order.DeliveredDate = deliveredDate;
            if (!string.IsNullOrEmpty(notes))
                order.Notes = notes;
            order.UpdatedAt = DateTime.UtcNow;

            Update(order);
            await SaveChanges();

            return true;
        }

        public async Task<List<OrderDTO>> GetAllOrdersWithDetails()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
                .Where(o => !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(order => new OrderDTO
            {
                OrderId = order.ID,
                UserId = order.UserID,
                UserName = order.User?.UserName ?? "Unknown",
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                ShippingAddress = order.ShippingAddress,
                City = order.City,
                PostalCode = order.PostalCode,
                Country = order.Country,
                PhoneNumber = order.PhoneNumber,
                ShippedDate = order.ShippedDate,
                DeliveredDate = order.DeliveredDate,
                Notes = order.Notes,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDTO
                {
                    OrderItemId = oi.ID,
                    OrderId = oi.OrderID,
                    ProductId = oi.ProductID,
                    ProductName = oi.Product?.Name ?? "Product",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList() ?? new List<OrderItemDTO>()
            }).ToList();
        }

        public async Task<decimal> GetOrderTotalAmount(int orderId)
        {
            var order = await GetById(orderId);
            return order?.TotalAmount ?? 0;
        }
    }
}

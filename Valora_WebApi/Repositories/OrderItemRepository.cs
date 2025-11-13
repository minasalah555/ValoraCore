using Microsoft.EntityFrameworkCore;
using Valora.Data;
using Valora.Models;

namespace Valora.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly Context _context;

        public OrderItemRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderId(int orderId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderID == orderId && !oi.IsDeleted)
                .ToListAsync();
        }

        public async Task<OrderItem> CreateOrderItem(int orderId, int productId, int quantity, decimal unitPrice)
        {
            var totalPrice = unitPrice * quantity;

            var orderItem = new OrderItem
            {
                OrderID = orderId,
                ProductID = productId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await Add(orderItem);
            await SaveChanges();

            return orderItem;
        }
    }
}

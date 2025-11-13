using Valora.Models;

namespace Valora.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<List<OrderItem>> GetOrderItemsByOrderId(int orderId);
        Task<OrderItem> CreateOrderItem(int orderId, int productId, int quantity, decimal unitPrice);
    }
}

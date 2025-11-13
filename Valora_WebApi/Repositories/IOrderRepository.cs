using Valora.DTOs;
using Valora.Models;

namespace Valora.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<OrderDTO?> GetOrderWithDetails(int orderId);
        Task<List<OrderDTO>> GetOrdersByUserId(string userId);
        Task<Order> CreateOrderFromCart(string userId, int cartId, string shippingAddress, string city, string postalCode, string country, string phoneNumber, string? notes);
        Task<bool> UpdateOrderStatus(int orderId, string status, DateTime? shippedDate, DateTime? deliveredDate, string? notes);
        Task<List<OrderDTO>> GetAllOrdersWithDetails();
        Task<decimal> GetOrderTotalAmount(int orderId);
    }
}

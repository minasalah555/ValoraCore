using Valora.DTOs;
using Valora.Models;
using Valora.Repositories;
using Valora.ViewModels;

namespace Valora.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderDTO?> GetOrderDetails(int orderId)
        {
            return await _orderRepository.GetOrderWithDetails(orderId);
        }

        public async Task<List<OrderDTO>> GetUserOrders(string userId)
        {
            return await _orderRepository.GetOrdersByUserId(userId);
        }

        public async Task<List<OrderDTO>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrdersWithDetails();
        }

        public async Task<Order> CreateOrder(CreateOrderViewModel model)
        {
            if (model.CartId == null || model.CartId == 0)
            {
                throw new Exception("Cart ID is required");
            }

            return await _orderRepository.CreateOrderFromCart(
                model.UserId,
                model.CartId.Value,
                model.ShippingAddress,
                model.City,
                model.PostalCode,
                model.Country,
                model.PhoneNumber,
                model.Notes
            );
        }

        public async Task<bool> UpdateOrderStatus(UpdateOrderStatusViewModel model)
        {
            return await _orderRepository.UpdateOrderStatus(
                model.OrderId,
                model.OrderStatus,
                model.ShippedDate,
                model.DeliveredDate,
                model.Notes
            );
        }

        public async Task<bool> CancelOrder(int orderId)
        {
            return await _orderRepository.UpdateOrderStatus(orderId, "Cancelled", null, null, "Order cancelled by user");
        }

        public async Task<decimal> GetOrderTotal(int orderId)
        {
            return await _orderRepository.GetOrderTotalAmount(orderId);
        }
    }
}

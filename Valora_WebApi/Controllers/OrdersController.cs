using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Valora.DTOs;
using Valora.Services;
using Valora.ViewModels;

namespace Valora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders (Admin only)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Orders/MyOrders (Get current user's orders)
        [HttpGet("MyOrders")]
        public async Task<ActionResult<List<OrderDTO>>> GetMyOrders()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                var orders = await _orderService.GetUserOrders(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Orders/{orderId}
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderDetails(orderId);
                if (order == null)
                {
                    return NotFound(new { message = "Order not found" });
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Orders/User/{userId} (Admin only)
        [HttpGet("User/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<OrderDTO>>> GetUserOrders(string userId)
        {
            try
            {
                var orders = await _orderService.GetUserOrders(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Orders/{orderId}/Total
        [HttpGet("{orderId}/Total")]
        public async Task<ActionResult> GetOrderTotal(int orderId)
        {
            try
            {
                var total = await _orderService.GetOrderTotal(orderId);
                return Ok(new { orderId = orderId, total = total });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Get userId from JWT token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                // Override userId from token (security measure)
                model.UserId = userId;

                var order = await _orderService.CreateOrder(model);
                return CreatedAtAction(nameof(GetOrder), new { orderId = order.ID }, new
                {
                    message = "Order created successfully",
                    orderId = order.ID,
                    orderNumber = order.OrderNumber,
                    totalAmount = order.TotalAmount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Orders/{orderId}/Status (Admin only)
        [HttpPut("{orderId}/Status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Set orderId from route parameter
                model.OrderId = orderId;

                var result = await _orderService.UpdateOrderStatus(model);
                if (result)
                {
                    return Ok(new { message = "Order status updated successfully" });
                }
                return NotFound(new { message = "Order not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Orders/{orderId}/Cancel
        [HttpPut("{orderId}/Cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                var result = await _orderService.CancelOrder(orderId);
                if (result)
                {
                    return Ok(new { message = "Order cancelled successfully" });
                }
                return NotFound(new { message = "Order not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

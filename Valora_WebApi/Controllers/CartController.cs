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
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        // GET: api/Cart/{cartId}
        [HttpGet("{cartId}")]
        public async Task<ActionResult<CartDTO>> GetCart(int cartId)
        {
            try
            {
                var cart = await _cartServices.ShowCartAsync(cartId);
                if (cart == null)
                {
                    return NotFound(new { message = "Cart not found" });
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Cart/MyCart (Get current user's cart)
        [HttpGet("MyCart")]
        public async Task<ActionResult<CartDTO>> GetMyCart()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                var cart = await _cartServices.showTheCartPerUser(userId);
                if (cart == null)
                {
                    return NotFound(new { message = "Cart not found for this user" });
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Cart/User/{userId}
        [HttpGet("User/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CartDTO>> GetCartByUserId(string userId)
        {
            try
            {
                var cart = await _cartServices.showTheCartPerUser(userId);
                if (cart == null)
                {
                    return NotFound(new { message = "Cart not found for this user" });
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Cart/Count (Get current user's cart item count)
        [HttpGet("Count")]
        public async Task<ActionResult<int>> GetMyCartItemCount()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                var count = await _cartServices.GetCartItemCountForUser(userId);
                return Ok(new { count = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Cart/AddItem
        [HttpPost("AddItem")]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Get userId from JWT token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                var cartId = await _cartServices.addToCart(
                    userId,
                    model.CartId ?? 0,
                    model.ProductId,
                    model.Quantity
                );

                return Ok(new
                {
                    message = "Item added to cart successfully",
                    cartId = cartId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Cart/RemoveItem/{productId}
        [HttpDelete("RemoveItem/{productId}")]
        public async Task<IActionResult> RemoveFromMyCart(int productId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                var cart = await _cartServices.showTheCartPerUser(userId);
                if (cart == null)
                    return NotFound(new { message = "Cart not found" });

                await _cartServices.RemoveItemFromCart(cart.CartId, productId);
                return Ok(new { message = "Item removed from cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Cart/Clear (Clear current user's cart)
        [HttpDelete("Clear")]
        public async Task<IActionResult> ClearMyCart()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                var cart = await _cartServices.showTheCartPerUser(userId);
                if (cart == null)
                    return NotFound(new { message = "Cart not found" });

                await _cartServices.DeleteCartAsync(cart.CartId);
                return Ok(new { message = "Cart cleared successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

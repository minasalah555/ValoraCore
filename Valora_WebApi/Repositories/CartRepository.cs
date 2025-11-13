using Valora.Models;
using Microsoft.EntityFrameworkCore;
using Valora.DTOs;
using Valora.Data;


namespace Valora.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly Context _context;
        public CartRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<int> AddToCart(string UserID, int cartId, int productId, int quantity)
        {
            Cart? cart = null;

            // If cartId is provided and greater than 0, try to get existing cart with tracking
            if (cartId > 0)
            {
                cart = await Query()
                    .Include(c => c.CartItems)
                    .AsTracking()
                    .FirstOrDefaultAsync(c => c.ID == cartId && !c.IsDeleted);
            }

            // If cart not found by ID, try to find by UserID
            if (cart == null)
            {
                cart = await Query()
                    .Include(c => c.CartItems)
                    .AsTracking()
                    .FirstOrDefaultAsync(c => c.UserID == UserID && !c.IsDeleted);
            }

            // If still no cart, create a new one
            if (cart == null)
            {
                cart = new Cart
                {
                    UserID = UserID,
                    CartItems = new List<CartItem>()
                };
                await Add(cart);
                // Persist immediately to generate a real ID so we can add items safely
                await SaveChanges();
                
                // Re-query the cart with tracking to ensure it's properly attached
                cart = await Query()
                    .Include(c => c.CartItems)
                    .AsTracking()
                    .FirstOrDefaultAsync(c => c.ID == cart.ID);
            }

            // Ensure CartItems is initialized
            cart.CartItems ??= new List<CartItem>();

            // Check if product already exists in cart
            var existingItem = cart.CartItems.FirstOrDefault(item => item.ProductID == productId);
            if (existingItem != null)
            {
                // Update quantity of existing item
                existingItem.Quantity += quantity;
            }
            else
            {
                // Add new item to cart
                var newItem = new CartItem
                {
                    ProductID = productId,
                    Quantity = quantity,
                    CartID = cart.ID
                };
                cart.CartItems.Add(newItem);
            }

            // Save changes - THIS IS THE KEY FIX!
            await SaveChanges();

            // Return the cart ID
            return cart.ID;
        }

        public async Task<CartDTO> ShowTheCart(int cartId)
        {
            var cart = await GetById(cartId);
            if (cart != null)
            {
                var cartDTO = new CartDTO
                {
                    UserId = cart.UserID,
                    CartId = cart.ID,
                    Items = (cart.CartItems ?? Enumerable.Empty<CartItem>()).Select(item => new CartItemDTO
                    {
                        ProductId = item.ProductID,
                        Quantity = item.Quantity
                    }).ToList()
                };
                return cartDTO;
            }
            else
            {
                return new CartDTO();
            }
        }

        public async Task<CartDTO> ShowTheCartByUserId(string userId)
        {
            var cart = await GetCartByUserId(userId);

            return new CartDTO
            {
                CartId = cart.ID,
                UserId = cart.UserID,
                Items = (cart.CartItems ?? Enumerable.Empty<CartItem>()).Select(item => new CartItemDTO
                {
                    ProductId = item.ProductID,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public async Task RemoveFromCart(int cartId, int productId, int quantity)
        {
            // Load the cart including CartItems with tracking so EF Core detects removals/changes
            var cart = await Query()
                .Include(c => c.CartItems)
                .AsTracking()
                .FirstOrDefaultAsync(c => c.ID == cartId && !c.IsDeleted);
            if (cart == null) return;
            
            cart.CartItems ??= new List<CartItem>();
            var existingItem = cart.CartItems.FirstOrDefault(item => item.ProductID == productId);
            if (existingItem != null)
            {
                existingItem.Quantity -= quantity;
                if (existingItem.Quantity <= 0)
                {
                    cart.CartItems.Remove(existingItem);
                }
                await SaveChanges();
            }
        }

        public override async Task<Cart?> GetById(int id)
        {
            return await Query()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);
        }

        public async Task saveTheCart()
        {
            await SaveChanges();
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            var cart = await Query()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId && !c.IsDeleted);
            if (cart != null)
            {
                return cart;
            }
            // If no cart exists for this user, create and persist one
            var newCart = new Cart
            {
                UserID = userId,
                CartItems = new List<CartItem>()
            };
            await Add(newCart);
            await SaveChanges();
            return newCart;
        }
    }
}

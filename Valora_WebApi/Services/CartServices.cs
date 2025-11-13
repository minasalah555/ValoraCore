using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valora.DTOs;
using Valora.Models;
using Valora.Repositories;

namespace Valora.Services
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartServices(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<int> addToCart(string UserID, int cartId, int productId, int quantity)
        {
            var resultingCartId = await _cartRepository.AddToCart(UserID, cartId, productId, quantity);
            await _cartRepository.SaveChanges();
            return resultingCartId;
        }

        public async Task<CartDTO> showTheCart(int cartId)
        {
            var dto = await _cartRepository.ShowTheCart(cartId);
            return dto ?? new CartDTO();
        }

        public async Task RemoveItemFromCart(int cartId, int productId)
        {
            // Delegate removal logic to the repository which handles persistence and quantity logic.
            // Use a large quantity so the repository will remove the item entirely (it subtracts the
            // provided quantity and removes when <= 0).
            await _cartRepository.RemoveFromCart(cartId, productId, int.MaxValue);
        }

        public async Task deleteFromCart(int cartId)
        {
            await _cartRepository.Delete(cartId);
            await _cartRepository.SaveChanges();
        }

        public async Task Add(Cart cart)
        {
            await _cartRepository.Add(cart);
            await _cartRepository.SaveChanges();
        }

        public async Task Update(Cart cart)
        {
            _cartRepository.Update(cart);
            await _cartRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _cartRepository.Delete(id);
            await _cartRepository.SaveChanges();
        }

        public async Task Save()
        {
            await _cartRepository.SaveChanges();
        }

        public async Task<CartDTO> showTheCartPerUser(string UserID)
        {
            var cart = await _cartRepository.GetCartByUserId(UserID);
            if (cart == null) return new CartDTO();
            return MapCart(cart);
        }

        // Backwards-compatible adapter used by controllers
        public async Task<CartDTO> ShowCartAsync(int cartId)
        {
            return await showTheCart(cartId);
        }

        public async Task DeleteCartAsync(int cartId)
        {
            await deleteFromCart(cartId);
        }

        public async Task<int> GetCartItemCountForUser(string userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            return (cart?.CartItems ?? new List<CartItem>()).Sum(ci => ci.Quantity);
        }

        private static CartDTO MapCart(Cart cart)
        {
            return new CartDTO
            {
                CartId = cart.ID,
                UserId = cart.UserID,
                Items = (cart.CartItems ?? new List<CartItem>()).Select(ci => new CartItemDTO
                {
                    ProductId = ci.ProductID,
                    Quantity = ci.Quantity
                }).ToList()
            };
        }
    }
}

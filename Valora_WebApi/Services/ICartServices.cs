using Valora.DTOs;
using Valora.Models;

namespace Valora.Services
{
    public interface ICartServices
    {
    // Adds item(s) to a cart and returns the cart id after operation (useful when cartId was not provided)
    public Task<int> addToCart(string UserID, int cartId, int productId, int quantity);
    public Task< CartDTO> showTheCart(int cartId);
    // Adapter methods used by controllers (async naming)
    public Task<CartDTO> ShowCartAsync(int cartId);
    public Task DeleteCartAsync(int cartId);
        // Remove a product entirely from a cart
        public Task RemoveItemFromCart(int cartId, int productId);
        public Task deleteFromCart(int cartId);
        Task Add(Cart cart);
        Task Update(Cart cart);
        Task Delete(int id);
        Task Save();
        public Task<CartDTO> showTheCartPerUser(string UserID);

    Task<int> GetCartItemCountForUser(string userId);







    }
}

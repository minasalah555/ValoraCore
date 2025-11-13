using Valora.DTOs;
using Valora.Models;

namespace Valora.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {

    // Adds item(s) to the user's cart. Returns the cart id after the operation (new or existing).
    public Task<int> AddToCart(string userId, int cartId, int productId, int quantity);
       public Task< CartDTO> ShowTheCart(int cartId);
        public Task RemoveFromCart(int cartId, int productId, int quantity);

        Task<Cart> GetCartByUserId(string userId);
    }
}

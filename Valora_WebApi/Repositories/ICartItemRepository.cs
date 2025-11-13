using Valora.Models;

namespace Valora.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<List<Product>> getItemsInCart(int cartId);
    }
}
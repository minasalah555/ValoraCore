using Microsoft.EntityFrameworkCore;
using Valora.Data;
using Valora.Models;
namespace Valora.Repositories

{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(Context context) : base(context)
        {
        }

        public async Task<List<Product>> getItemsInCart(int cartId)
        {
            var products = await Query()
                .Include(ci => ci.Product)
                .Where(ci => ci.CartID == cartId && !ci.IsDeleted)
                .Select(ci => ci.Product!)
                .ToListAsync();

            return products;
        }
    }
}

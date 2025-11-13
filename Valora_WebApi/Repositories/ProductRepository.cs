using Microsoft.EntityFrameworkCore;
using Valora.Data;
using Valora.Models;

namespace Valora.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Valora.Data;
using Valora.Models;

namespace Valora.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Category?> GetCategoryWithProducts(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);
        }
    }
}

using Valora.Models;

namespace Valora.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetCategoryWithProducts(int id);
    }
}

using Valora.Models;

namespace Valora.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
    }
}

using Valora.Models;

namespace Valora.Services
{
    public interface IProductServices
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task Add(Product product);
        void Update(Product product);
        Task Delete(int id);
        Task Save();
    }
}

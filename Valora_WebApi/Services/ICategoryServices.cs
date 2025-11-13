using Valora.Models;

namespace Valora.Services
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task<Category?> GetCategoryWithProducts(int id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
        Task Save();
    }
}

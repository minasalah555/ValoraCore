using Microsoft.EntityFrameworkCore;
using Valora.Models;
using Valora.Repositories;

namespace Valora.Services
{
    public class CategoryServices
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryServices(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<Category>> GetAll() => await _categoryRepo.GetAll().ToListAsync();
        public async Task<Category?> GetById(int id) => await _categoryRepo.GetById(id);
        public async Task<Category?> GetCategoryWithProducts(int id) => await _categoryRepo.GetCategoryWithProducts(id);
        public async Task Add(Category category) => await _categoryRepo.Add(category);
        public async Task Update(Category category) => _categoryRepo.Update(category);
        public async Task Delete(int id) => await _categoryRepo.Delete(id);
        public async Task Save() => await _categoryRepo.SaveChanges();
    }
}

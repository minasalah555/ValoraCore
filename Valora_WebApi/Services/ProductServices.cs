using Microsoft.EntityFrameworkCore;
using Valora.Models;
using Valora.Repositories;

namespace Valora.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepo;

        public ProductServices(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<List<Product>> GetAll() => await _productRepo.GetAll().ToListAsync();
        public async Task<Product?> GetById(int id) => await _productRepo.GetById(id);
        public async Task Add(Product product) => await _productRepo.Add(product);
        public void Update(Product product) => _productRepo.Update(product);
        public async Task Delete(int id) => await _productRepo.Delete(id);
        public async Task Save() => await _productRepo.SaveChanges();
    }
}

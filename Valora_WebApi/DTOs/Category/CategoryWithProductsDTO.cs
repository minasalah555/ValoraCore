using Valora.DTOs.Product;

namespace Valora.DTOs.Category
{
    /// <summary>
    /// DTO for Category with its Products list (prevents circular reference)
    /// </summary>
    public class CategoryWithProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductReadDTO> Products { get; set; } = new List<ProductReadDTO>();
        public int ProductCount => Products?.Count ?? 0; 
    }
}

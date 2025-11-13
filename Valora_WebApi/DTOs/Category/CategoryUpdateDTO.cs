using Valora.DTOs.Product;

namespace Valora.DTOs.Category
{
    public class CategoryUpdateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductReadDTO>? Products { get; set; }
    }
}

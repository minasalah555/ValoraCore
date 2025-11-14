using System.ComponentModel.DataAnnotations;

namespace Valora.DTOs.Product
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1,000,000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, 100000, ErrorMessage = "Stock quantity must be between 0 and 100,000")]
        public int StockQuantity { get; set; }

        [StringLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string? ImgUrl { get; set; }

        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number")]
        public int CategoryId { get; set; }
    }
}

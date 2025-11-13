using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class AddToCartViewModel
    {
        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        // Optional - if provided, adds to existing cart, otherwise creates new
        public int? CartId { get; set; }
    }
}

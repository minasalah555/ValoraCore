using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class CreateOrderViewModel
    {
        // UserId will be set automatically from JWT token
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shipping address is required")]
        [MaxLength(200, ErrorMessage = "Shipping address cannot exceed 200 characters")]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal code is required")]
        [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }

        // Optional - if not provided, will use user's current cart
        public int? CartId { get; set; }
    }
}

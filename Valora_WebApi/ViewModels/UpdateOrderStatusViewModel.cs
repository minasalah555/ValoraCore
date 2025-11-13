using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class UpdateOrderStatusViewModel
    {
        // OrderId will be set from route parameter
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Order status is required")]
        [MaxLength(50, ErrorMessage = "Order status cannot exceed 50 characters")]
        public string OrderStatus { get; set; } = "Pending";

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}

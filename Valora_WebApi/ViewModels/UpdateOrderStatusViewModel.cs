using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class UpdateOrderStatusViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Valora.Models
{
    public class Order : BaseModel
    {
        [ForeignKey("User")]
        [Required]
        public string UserID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderStatus { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered, Cancelled

        [MaxLength(200)]
        public string? ShippingAddress { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
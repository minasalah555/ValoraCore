using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Valora.Models
{
    public class OrderItem : BaseModel
    {
        [ForeignKey("Order")]
        [Required]
        public int OrderID { get; set; }

        [ForeignKey("Product")]
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        // Navigation Properties
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}

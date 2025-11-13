using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Valora.Models
{
    public class Review : BaseModel
    {
        [ForeignKey("Product")]
        [Required]
        public int ProductID { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserID { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        public bool IsVerifiedPurchase { get; set; } = false;

        // Navigation Properties
        public virtual Product? Product { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}

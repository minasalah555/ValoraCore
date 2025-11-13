using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class UpdateReviewViewModel
    {
        [Required]
        public int ReviewId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}

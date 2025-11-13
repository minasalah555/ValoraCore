using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Valora.Models
{
    public class Product: BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public string? ImgUrl { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace Valora.Models
{
    public class Cart : Models.BaseModel
    {
        [ForeignKey("User")]
        public string UserID { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual List<CartItem>? CartItems { get; set; } 
    }
}

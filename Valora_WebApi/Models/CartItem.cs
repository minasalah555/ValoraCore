using System.ComponentModel.DataAnnotations.Schema;

namespace Valora.Models
{
    public class CartItem :BaseModel
    {
        [ForeignKey("Cart")]

         public int CartID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual Product? Product { get; set; }
    }
}
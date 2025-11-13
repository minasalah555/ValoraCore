using System.ComponentModel.DataAnnotations.Schema;


namespace Valora.Models
{
    public class Category: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
         public List<Product> Products { get; set; }
    }
}

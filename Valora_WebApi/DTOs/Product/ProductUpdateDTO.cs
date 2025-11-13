namespace Valora.DTOs.Product
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImgUrl { get; set; }
        public int CategoryId { get; set; }
    }
}

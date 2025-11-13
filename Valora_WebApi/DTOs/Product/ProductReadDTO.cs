namespace Valora.DTOs.Product
{
    public class ProductReadDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImgUrl { get; set; }
        public string CategoryName { get; set; }
    }
}

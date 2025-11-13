namespace Valora.DTOs
{
    public class CartItemDTO
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductImage { get; set; }
        public decimal TotalPrice => ProductPrice * Quantity;
    }
}
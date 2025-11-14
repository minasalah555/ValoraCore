namespace Valora.DTOs
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string? ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal => ProductPrice * Quantity;
    }
}
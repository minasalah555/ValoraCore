namespace Valora.DTOs
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<CartItemDTO> Items { get; set; } = new();
    }
}

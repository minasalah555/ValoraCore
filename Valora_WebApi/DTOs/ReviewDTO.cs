namespace Valora.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool IsVerifiedPurchase { get; set; }
    }
}
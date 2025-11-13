using Microsoft.EntityFrameworkCore;
using Valora.Data;
using Valora.DTOs;
using Valora.Models;

namespace Valora.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly Context _context;

        public ReviewRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ReviewDTO>> GetReviewsByProductId(int productId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductID == productId && !r.IsDeleted)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();

            return reviews.Select(review => new ReviewDTO
            {
                ReviewId = review.ID,
                ProductId = review.ProductID,
                ProductName = review.Product?.GetType().GetProperty("Name")?.GetValue(review.Product)?.ToString() ?? "Product",
                UserId = review.UserID,
                UserName = review.User?.UserName ?? "Anonymous",
                Rating = review.Rating,
                Title = review.Title,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                IsVerifiedPurchase = review.IsVerifiedPurchase
            }).ToList();
        }

        public async Task<List<ReviewDTO>> GetReviewsByUserId(string userId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.UserID == userId && !r.IsDeleted)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();

            return reviews.Select(review => new ReviewDTO
            {
                ReviewId = review.ID,
                ProductId = review.ProductID,
                ProductName = review.Product?.GetType().GetProperty("Name")?.GetValue(review.Product)?.ToString() ?? "Product",
                UserId = review.UserID,
                UserName = review.User?.UserName ?? "Anonymous",
                Rating = review.Rating,
                Title = review.Title,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                IsVerifiedPurchase = review.IsVerifiedPurchase
            }).ToList();
        }

        public async Task<ReviewDTO?> GetReviewWithDetails(int reviewId)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.ID == reviewId && !r.IsDeleted);

            if (review == null)
                return null;

            return new ReviewDTO
            {
                ReviewId = review.ID,
                ProductId = review.ProductID,
                ProductName = review.Product?.GetType().GetProperty("Name")?.GetValue(review.Product)?.ToString() ?? "Product",
                UserId = review.UserID,
                UserName = review.User?.UserName ?? "Anonymous",
                Rating = review.Rating,
                Title = review.Title,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                IsVerifiedPurchase = review.IsVerifiedPurchase
            };
        }

        public async Task<bool> HasUserPurchasedProduct(string userId, int productId)
        {
            var hasPurchased = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserID == userId && !o.IsDeleted)
                .AnyAsync(o => o.OrderItems!.Any(oi => oi.ProductID == productId && !oi.IsDeleted));

            return hasPurchased;
        }

        public async Task<double> GetAverageRatingForProduct(int productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductID == productId && !r.IsDeleted)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return reviews.Average(r => r.Rating);
        }

        public async Task<int> GetReviewCountForProduct(int productId)
        {
            return await _context.Reviews
                .Where(r => r.ProductID == productId && !r.IsDeleted)
                .CountAsync();
        }
    }
}

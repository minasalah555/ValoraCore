using Valora.DTOs;
using Valora.Models;

namespace Valora.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<ReviewDTO>> GetReviewsByProductId(int productId);
        Task<List<ReviewDTO>> GetReviewsByUserId(string userId);
        Task<ReviewDTO?> GetReviewWithDetails(int reviewId);
        Task<bool> HasUserPurchasedProduct(string userId, int productId);
        Task<double> GetAverageRatingForProduct(int productId);
        Task<int> GetReviewCountForProduct(int productId);
    }
}

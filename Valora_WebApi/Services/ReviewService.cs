using Valora.DTOs;
using Valora.Models;
using Valora.Repositories;
using Valora.ViewModels;

namespace Valora.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<ReviewDTO>> GetProductReviews(int productId)
        {
            return await _reviewRepository.GetReviewsByProductId(productId);
        }

        public async Task<List<ReviewDTO>> GetUserReviews(string userId)
        {
            return await _reviewRepository.GetReviewsByUserId(userId);
        }

        public async Task<ReviewDTO?> GetReviewDetails(int reviewId)
        {
            return await _reviewRepository.GetReviewWithDetails(reviewId);
        }

        public async Task<Review> CreateReview(CreateReviewViewModel model)
        {
            // Check if user has purchased the product
            var hasPurchased = await _reviewRepository.HasUserPurchasedProduct(model.UserId, model.ProductId);

            var review = new Review
            {
                ProductID = model.ProductId,
                UserID = model.UserId,
                Rating = model.Rating,
                Title = model.Title,
                Comment = model.Comment,
                ReviewDate = DateTime.UtcNow,
                IsVerifiedPurchase = hasPurchased,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _reviewRepository.Add(review);
            await _reviewRepository.SaveChanges();

            return review;
        }

        public async Task<bool> UpdateReview(UpdateReviewViewModel model)
        {
            var review = await _reviewRepository.GetByIDWithTracking(model.ReviewId);
            if (review == null)
                return false;

            review.Rating = model.Rating;
            review.Title = model.Title;
            review.Comment = model.Comment;
            review.UpdatedAt = DateTime.UtcNow;

            _reviewRepository.Update(review);
            await _reviewRepository.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteReview(int reviewId)
        {
            await _reviewRepository.Delete(reviewId);
            await _reviewRepository.SaveChanges();
            return true;
        }

        public async Task<double> GetProductAverageRating(int productId)
        {
            return await _reviewRepository.GetAverageRatingForProduct(productId);
        }

        public async Task<int> GetProductReviewCount(int productId)
        {
            return await _reviewRepository.GetReviewCountForProduct(productId);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Valora.DTOs;
using Valora.Services;
using Valora.ViewModels;

namespace Valora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewsController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/Reviews/Product/{productId}
        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<List<ReviewDTO>>> GetProductReviews(int productId)
        {
            try
            {
                var reviews = await _reviewService.GetProductReviews(productId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Reviews/MyReviews (Get current user's reviews)
        [HttpGet("MyReviews")]
        [Authorize]
        public async Task<ActionResult<List<ReviewDTO>>> GetMyReviews()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                var reviews = await _reviewService.GetUserReviews(userId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Reviews/User/{userId}
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<List<ReviewDTO>>> GetUserReviews(string userId)
        {
            try
            {
                var reviews = await _reviewService.GetUserReviews(userId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Reviews/{reviewId}
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int reviewId)
        {
            try
            {
                var review = await _reviewService.GetReviewDetails(reviewId);
                if (review == null)
                {
                    return NotFound(new { message = "Review not found" });
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Reviews/Product/{productId}/Rating
        [HttpGet("Product/{productId}/Rating")]
        public async Task<ActionResult> GetProductRating(int productId)
        {
            try
            {
                var averageRating = await _reviewService.GetProductAverageRating(productId);
                var reviewCount = await _reviewService.GetProductReviewCount(productId);

                return Ok(new
                {
                    productId = productId,
                    averageRating = averageRating,
                    reviewCount = reviewCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Reviews
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateReview([FromBody] CreateReviewViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Get userId from JWT token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "User not authenticated" });

                // Override userId from token (security measure)
                model.UserId = userId;

                var review = await _reviewService.CreateReview(model);
                return CreatedAtAction(nameof(GetReview), new { reviewId = review.ID }, new
                {
                    message = "Review created successfully",
                    reviewId = review.ID
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Reviews/{reviewId}
        [HttpPut("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] UpdateReviewViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Set the reviewId from route parameter
                model.ReviewId = reviewId;

                var result = await _reviewService.UpdateReview(model);
                if (result)
                {
                    return Ok(new { message = "Review updated successfully" });
                }
                return NotFound(new { message = "Review not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Reviews/{reviewId}
        [HttpDelete("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            try
            {
                var result = await _reviewService.DeleteReview(reviewId);
                if (result)
                {
                    return Ok(new { message = "Review deleted successfully" });
                }
                return NotFound(new { message = "Review not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

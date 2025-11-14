using AutoMapper;
using Valora.DTOs;
using Valora.Models;
using Valora.ViewModels;

namespace Valora.DTOs.AutoMapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            // Review -> ReviewDTO
            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : ""))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : ""));

            // CreateReviewViewModel -> Review
            CreateMap<CreateReviewViewModel, Review>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            // UpdateReviewViewModel -> Review
            CreateMap<UpdateReviewViewModel, Review>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ReviewId))
                .ForMember(dest => dest.ProductID, opt => opt.Ignore())
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerifiedPurchase, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}

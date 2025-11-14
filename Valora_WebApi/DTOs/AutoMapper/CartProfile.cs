using AutoMapper;
using Valora.DTOs;
using Valora.Models;
using Valora.ViewModels;

namespace Valora.DTOs.AutoMapper
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            // Cart -> CartDTO
            CreateMap<Cart, CartDTO>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems))
                .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.CartItems != null ? src.CartItems.Count : 0))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => 
                    src.CartItems != null 
                        ? src.CartItems.Where(ci => !ci.IsDeleted && ci.Product != null)
                            .Sum(ci => ci.Product!.Price * ci.Quantity) 
                        : 0));

            // CartItem -> CartItemDTO
            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : ""))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product != null ? src.Product.Price : 0))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product != null ? src.Product.ImgUrl : null));

            // AddToCartViewModel -> CartItem
            CreateMap<AddToCartViewModel, CartItem>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CartID, opt => opt.MapFrom(src => src.CartId ?? 0))
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Cart, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
}

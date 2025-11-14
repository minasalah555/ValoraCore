using AutoMapper;
using Valora.DTOs;
using Valora.Models;
using Valora.ViewModels;

namespace Valora.DTOs.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Order -> OrderDTO
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : ""))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            // OrderItem -> OrderItemDTO
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.OrderItemId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : ""))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product != null ? src.Product.ImgUrl : null));

            // CreateOrderViewModel -> Order
            CreateMap<CreateOrderViewModel, Order>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => "Pending"))
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.ShippedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeliveredDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

            // UpdateOrderStatusViewModel -> Order
            CreateMap<UpdateOrderStatusViewModel, Order>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore())
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore())
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
        }
    }
}

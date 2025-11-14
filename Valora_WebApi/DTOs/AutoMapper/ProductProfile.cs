using AutoMapper;
using Valora.DTOs.Product;

namespace Valora.DTOs.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product -> ProductReadDTO (prevents circular reference by excluding Category navigation)
            CreateMap<Models.Product, ProductReadDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

            // ProductCreateDTO -> Product
            CreateMap<ProductCreateDTO, Models.Product>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // ProductUpdateDTO -> Product
            CreateMap<ProductUpdateDTO, Models.Product>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());
        }
    }
}

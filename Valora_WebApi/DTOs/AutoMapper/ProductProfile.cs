using AutoMapper;
using Valora.DTOs.Product;

namespace Valora.DTOs.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product -> ProductReadDTO
            CreateMap<Models.Product, ProductReadDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""));

            // ProductCreateDTO -> Product
            CreateMap<ProductCreateDTO, Models.Product>();

            // ProductUpdateDTO -> Product
            CreateMap<ProductUpdateDTO, Models.Product>();
        }
    }
}

using AutoMapper;
using Valora.DTOs.Category;

namespace Valora.DTOs.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Category -> CategoryReadDTO
            CreateMap<Models.Category, CategoryReadDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));

            // Category -> CategoryWithProductsDTO (prevents circular reference)
            CreateMap<Models.Category, CategoryWithProductsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            // CategoryCreateDTO -> Category
            CreateMap<CategoryCreateDTO, Models.Category>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.Ignore());

            // CategoryUpdateDTO -> Category
            CreateMap<CategoryUpdateDTO, Models.Category>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.Ignore());
        }
    }
}

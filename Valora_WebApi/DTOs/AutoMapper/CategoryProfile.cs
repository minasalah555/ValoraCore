using AutoMapper;
using Valora.DTOs.Category;

namespace Valora.DTOs.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Category -> CategoryReadDTO
            CreateMap<Models.Category, CategoryReadDTO>();

            // CategoryCreateDTO -> Category
            CreateMap<CategoryCreateDTO, Models.Category>();

            // CategoryUpdateDTO -> Category
            CreateMap<CategoryUpdateDTO, Models.Category>();
        }
    }
}

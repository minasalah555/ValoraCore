using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Valora.DTOs.Category;
using Valora.Models;
using Valora.Services;

namespace Valora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetCategories()
        {
            var categories = await _categoryServices.GetAll();
            var categoryDTOs = _mapper.Map<List<CategoryReadDTO>>(categories);
            return Ok(categoryDTOs);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDTO>> GetCategory(int id)
        {
            var category = await _categoryServices.GetById(id);

            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            var categoryDTO = _mapper.Map<CategoryReadDTO>(category);
            return Ok(categoryDTO);
        }

        // GET: api/Categories/5/Products
        [HttpGet("{id}/Products")]
        public async Task<ActionResult> GetCategoryWithProducts(int id)
        {
            var category = await _categoryServices.GetCategoryWithProducts(id);

            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryReadDTO>> CreateCategory([FromBody] CategoryCreateDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(categoryDTO);
            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;

            await _categoryServices.Add(category);
            await _categoryServices.Save();

            var categoryReadDTO = _mapper.Map<CategoryReadDTO>(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.ID }, categoryReadDTO);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCategory = await _categoryServices.GetById(id);
            if (existingCategory == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            _mapper.Map(categoryDTO, existingCategory);
            existingCategory.UpdatedAt = DateTime.UtcNow;

            await _categoryServices.Update(existingCategory);
            await _categoryServices.Save();

            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryServices.GetById(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            await _categoryServices.Delete(id);
            await _categoryServices.Save();

            return NoContent();
        }
    }
}

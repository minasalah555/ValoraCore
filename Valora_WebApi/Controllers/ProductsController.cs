using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Valora.DTOs.Product;
using Valora.Models;
using Valora.Services;

namespace Valora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;

        public ProductsController(IProductServices productServices, IMapper mapper)
        {
            _productServices = productServices;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProducts()
        {
            var products = await _productServices.GetAll();
            var productDTOs = _mapper.Map<List<ProductReadDTO>>(products);
            return Ok(productDTOs);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDTO>> GetProduct(int id)
        {
            var product = await _productServices.GetById(id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            var productDTO = _mapper.Map<ProductReadDTO>(product);
            return Ok(productDTO);
        }

        // POST: api/Products
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductReadDTO>> CreateProduct([FromBody] ProductCreateDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(productDTO);
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            await _productServices.Add(product);
            await _productServices.Save();

            var productReadDTO = _mapper.Map<ProductReadDTO>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ID }, productReadDTO);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = await _productServices.GetById(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            _mapper.Map(productDTO, existingProduct);
            existingProduct.UpdatedAt = DateTime.UtcNow;

            _productServices.Update(existingProduct);
            await _productServices.Save();

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productServices.GetById(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            await _productServices.Delete(id);
            await _productServices.Save();

            return NoContent();
        }
    }
}

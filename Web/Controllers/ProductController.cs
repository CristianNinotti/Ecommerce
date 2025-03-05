using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController (IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("All Products")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProductsService();
            return Ok(products);
        }
        [HttpGet("ProductById/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult GetProductById([FromRoute]int id)
        {
            try
            {
                var product = _productService.GetProductByIdService(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound($"Producto con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
        [HttpPost("Create Product")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult CreateProduct([FromBody] ProductRequest product)
        {
            _productService.CreateProductService(product);
            return Ok("Producto Creado");
        }
        [HttpPut("UpdateProduct/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult UpdateProduct([FromRoute]int id, ProductRequest product)
        {
            try
            {
                var UpdatedProduct = _productService.ToUpdateProductService(id, product);
                return Ok(UpdatedProduct);
            }
            catch (Exception ex)
            {
                return NotFound($"Producto con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteProduct/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult DeleteProduct([FromRoute]int id)
        {
            try
            {
                _productService.DeleteProductService(id);
                return Ok("Producto Eliminado");
            }
            catch (Exception ex)
            {
                return NotFound($"Producto con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
    }
}

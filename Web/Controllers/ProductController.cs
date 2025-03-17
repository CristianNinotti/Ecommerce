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
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("All Products Available")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult GetAllProductsAvailable()
        {
            var products = _productService.GetAllProducts().Where(o=>o.Available);
            return Ok(products);
        }

        [HttpGet("ProductById/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult GetProductById([FromRoute]int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound($"Producto con ID {id} no encontrado. Error: {ex.Message}");
            }
        }

        [HttpGet("ProductsByCategory/{categoryId}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult GetProductsByCategoryId([FromRoute] int categoryId)
        {
            try
            {
                // Llamar al servicio que obtiene los productos por CategoryId
                var products = _productService.GetProductsByCategoryId(categoryId);

                // Verificar si todos los productos tienen Available = false
                if (products == null || !products.Any())
                {
                    return NotFound($"No se encontraron productos para la categoría con ID {categoryId}.");
                }

                // Si todos los productos están marcados como no disponibles, no mostrar la categoría
                if (products.All(p => p.Available == false))
                {
                    return NotFound($"La categoría con ID {categoryId} no tiene productos disponibles.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound($"Error al obtener productos para la categoría con ID {categoryId}. Error: {ex.Message}");
            }
        }


        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] ProductRequest product)
        {
            var (success, message) = _productService.CreateProduct(product);

            if (!success) return BadRequest(new { success, message });

            return Ok(new { success, message });
        }

        [HttpPut("UpdateProduct/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult UpdateProduct([FromRoute]int id, ProductRequest product)
        {
            try
            {
                var UpdatedProduct = _productService.ToUpdateProduct(id, product);
                return Ok(UpdatedProduct);
            }
            catch (Exception ex)
            {
                return NotFound($"Producto con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
        [HttpDelete("SoftDeleteProduct/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult SoftDeleteProduct([FromRoute]int id)
        {
            try
            {
                _productService.SoftDeleteProduct(id);
                return Ok("Producto Eliminado");
            }
            catch (Exception ex)
            {
                return NotFound($"Producto con ID {id} no encontrado. Error: {ex.Message}");
            }
        }

        [HttpDelete("HardDeleteProduct/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult HardDeleteProduct([FromRoute] int id)
        {
            try
            {
                _productService.HardDeleteProduct(id);
                return Ok("Producto Eliminado");
            }
            catch (Exception ex)
            {
                return NotFound($"Producto con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
    }
}

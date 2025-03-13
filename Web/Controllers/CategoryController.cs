using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Models.Response;
using Application.Models.Request;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet("All Categories")]
        [Authorize(Policy = "SuperAdminOnly")]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryService.GetAllCategories());
        }

        [HttpGet("CategoryId/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public ActionResult<CategoryResponse?> GetCategoryById([FromRoute] int id)
        {
            return Ok(_categoryService.GetCategoryById(id));
        }
        [HttpGet("CategoryWithProducts/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public ActionResult<ProductResponse?> GetAllProducts([FromRoute] int id)
        {
            // Filtra los productos por CategoryId
            var productInCategory = _productService.GetAllProducts()
                                                    .Where(m => m.CategoryId == id)  // Filtra los productos por CategoryId
                                                    .ToList(); // Asegúrate de materializar la lista

            return Ok(productInCategory);
        }

        [HttpPost("CreateCategory")]

        public IActionResult CreateCategory([FromBody] CategoryRequest request)
        {
            _categoryService.CreateCategory(request);
            return Ok();
        }

        [HttpPut("UpdateCategory/{id}")]
        [Authorize(Policy="SuperAdminOnly")]

        public ActionResult<bool> UpdateCategory([FromRoute]int id ,[FromBody] CategoryRequest request)
        {
            
            return Ok(_categoryService.UpdateCategory(id, request));
        }

        [HttpDelete("DeleteCategory/{id}")]
        [Authorize(Policy ="SuperAdminOnly")]
        public ActionResult<bool> DeleteCategory([FromRoute]int id)
        {
            return Ok(_categoryService.DeleteCategory(id));
        }
    }
}

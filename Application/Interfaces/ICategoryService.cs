using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryResponse> GetAllCategories();
        CategoryResponse? GetCategoryById(int id);
        CategoryResponse CreateCategory(CategoryRequest category);
        CategoryResponse UpdateCategory(int id, CategoryRequest category);
        CategoryResponse DeleteCategory(int id);


        
    }
}

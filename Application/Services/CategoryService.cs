using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService (ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryResponse> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            return categories.Select(CategoryProfile.ToCategoryResponse).ToList();
        }

        public CategoryResponse? GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category != null)
            {
                return CategoryProfile.ToCategoryResponse(category);
            }
            return null;
        }

        public CategoryResponse CreateCategory(CategoryRequest category)
        {
            var CategoryEntity = CategoryProfile.ToCategoryEntity(category);
            _categoryRepository.CreateCategory(CategoryEntity);
            return CategoryProfile.ToCategoryResponse(CategoryEntity);
        }

        public CategoryResponse UpdateCategory(int id, CategoryRequest category)
        {
            var CategoryEntity = _categoryRepository.GetCategoryById(id);
            if (CategoryEntity == null)
            {
                throw new Exception("Categoria no encontrada");
            }
            CategoryProfile.ToCategoryEntityUpdate(CategoryEntity, category);
            _categoryRepository.UpdateCategory(CategoryEntity);
            return CategoryProfile.ToCategoryResponse(CategoryEntity);
        }

        public CategoryResponse DeleteCategory(int id)
        {
            var CategoryEntity = _categoryRepository.GetCategoryById(id);
            if(CategoryEntity == null)
            {
                throw new Exception("Esa categoria, no existe");
            }
            _categoryRepository.DeleteCategory(CategoryEntity);
            return CategoryProfile.ToCategoryResponse(CategoryEntity);
        }


    }
}

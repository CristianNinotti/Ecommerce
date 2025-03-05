using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Context;

namespace Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceDbContext _category;

        public CategoryRepository(EcommerceDbContext category)
        {
            _category = category;
        }

        public List<Category> GetAllCategories()
        {
            return _category.Categories.ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _category.Categories.FirstOrDefault(m => m.Id == id);
        }

        public void CreateCategory(Category category)
        {
            _category.Categories.Add(category);
            _category.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _category.Categories.Update(category);
            _category.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            _category.Categories.Remove(category);
            _category.SaveChanges();
        }
    }
}

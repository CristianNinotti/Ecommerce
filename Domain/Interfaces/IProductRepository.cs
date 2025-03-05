using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProductsRepository();
        Product? GetProductByIdRepository(int id);
        void CreateProductRepository(Product product);
        void UpdateProductRepository(Product product);
        void DeleteProductRepository(Product product);
    }
}

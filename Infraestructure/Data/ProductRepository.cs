using Infraestructure.Context;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _product;
        public ProductRepository(EcommerceDbContext product)
        {
            _product = product;
        }
        public List<Product> GetAllProductsRepository( )
        {
            return _product.Products.ToList();
        }
        public Product? GetProductByIdRepository(int id )
        {
            return _product.Products.FirstOrDefault(m => m.Id == id);
        }
        public void CreateProductRepository(Product product)
        {
            _product.Products.Add(product);
            _product.SaveChanges();
        }
        public void UpdateProductRepository(Product product)
        {
            _product.Products.Update(product);
            _product.SaveChanges();
        }
        public void DeleteProductRepository(Product product)
        {
            _product.Products.Remove(product);
            _product.SaveChanges();
        }
    }
}

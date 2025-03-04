using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

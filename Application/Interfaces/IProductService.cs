using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProductsService();
        ProductResponse GetProductByIdService(int id);
        ProductResponse CreateProductService(ProductRequest product);
        ProductResponse ToUpdateProductService(int id, ProductRequest product);
        ProductResponse DeleteProductService(int id);
    }
}

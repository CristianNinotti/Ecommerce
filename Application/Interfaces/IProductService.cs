using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<ProductResponse> GetAllProducts();
        ProductResponse? GetProductById(int id);
        ProductResponse CreateProduct(ProductRequest product);
        ProductResponse ToUpdateProduct(int id, ProductRequest product);
        ProductResponse DeleteProduct(int id);
    }
}

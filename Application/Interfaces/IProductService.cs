using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<ProductResponse> GetAllProductsService();
        ProductResponse GetProductByIdService(int id);
        ProductResponse CreateProductService(ProductRequest product);
        ProductResponse ToUpdateProductService(int id, ProductRequest product);
        ProductResponse DeleteProductService(int id);
    }
}

using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<ProductResponse> GetAllProducts();
        ProductResponse? GetProductById(int id);
        void CreateProduct(ProductRequest product);
        bool ToUpdateProduct(int id, ProductRequest product);
        bool SoftDeleteProduct(int id);
        bool HardDeleteProduct(int id);
    }
}

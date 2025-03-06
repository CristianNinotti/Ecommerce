using Domain.Interfaces;
using Application.Mappings;
using Application.Models.Response;
using Application.Models.Request;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<ProductResponse> GetAllProducts()
        {
            var products = _productRepository.GetAllProductsRepository();
            return ProductProfile.ToProductResponse(products);
        }

        public ProductResponse? GetProductById(int id)
        {
            var product = _productRepository.GetProductByIdRepository(id);
            if (product != null)
            {
                return ProductProfile.ToProductResponse(product);
            }
            return null;
        }

        public void CreateProduct(ProductRequest product)
        {
            var productEntity = ProductProfile.ToProductEntity(product);
            _productRepository.CreateProductRepository(productEntity);
        }
        public bool ToUpdateProduct(int id, ProductRequest request)
        {
            var productEntity = _productRepository.GetProductByIdRepository(id);
            if (productEntity == null)
                {
                return false;
                }
            ProductProfile.ToProductUpdate(productEntity, request);
            _productRepository.UpdateProductRepository(productEntity);
            return true;
        }
        public bool DeleteProduct(int id)
        {
            var productEntity = _productRepository.GetProductByIdRepository(id);
            if (productEntity == null)
            {
                return false;
            }
            _productRepository.DeleteProductRepository(productEntity);
            return true;
        }
    }
}

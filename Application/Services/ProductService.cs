using Domain.Interfaces;
using Application.Mappings;
using Application.Models.Response;
using Application.Models.Request;
using Application.Interfaces;
using Domain.Entities;
using System.Runtime.InteropServices;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IOrderItemRepository orderItemRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderItemRepository = orderItemRepository;
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
            var category = _categoryRepository.GetCategoryById(product.CategoryId);
            if (category != null && category.Available == true)
            {
                var productEntity = ProductProfile.ToProductEntity(product);
                productEntity.Categoria = category;
                _productRepository.CreateProductRepository(productEntity);
            }
        }
        public bool ToUpdateProduct(int id, ProductRequest request)
        {
            var productEntity = _productRepository.GetProductByIdRepository(id);
            var category = _categoryRepository.GetCategoryById(request.CategoryId);
            if (category != null && productEntity != null && productEntity.Available == true)
            {
                ProductProfile.ToProductUpdate(productEntity, request);
                productEntity.Categoria = category;
                _productRepository.UpdateProductRepository(productEntity);
                return true;
            }
            return false;
        }
        public bool SoftDeleteProduct(int id)
        {
            var productEntity = _productRepository.GetProductByIdRepository(id);
            if (productEntity == null)
            {
                return false;
            }
            productEntity.Available = false;
            _productRepository.UpdateProductRepository(productEntity);
            var orderItems = _orderItemRepository.GetAllOrderItemsByProductIdRepository(id);
            foreach(var orderItem in orderItems)
            {
                orderItem.Available = false;
                _orderItemRepository.UpdateOrderItemRepository(orderItem);
            }
            return true;
        }

        public bool HardDeleteProduct(int id)
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

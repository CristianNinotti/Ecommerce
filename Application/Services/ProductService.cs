using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Application.Mappings;
using Application.Models.Response;
using Application.Models.Request;
using Application.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> GetAllProductsService()
        {
            var products = _productRepository.GetAllProductsRepository();
            return products.ToList();
        }

        public ProductResponse GetProductByIdService(int id)
        {
            var product = _productRepository.GetProductByIdRepository(id);
            return ProductProfile.ToProductResponse(product);
        }

        public ProductResponse CreateProductService(ProductRequest product)
        {
            var productEntity = ProductProfile.ToProductEntity(product);
            _productRepository.CreateProductRepository(productEntity);
            return ProductProfile.ToProductResponse(productEntity);
        }
        public ProductResponse ToUpdateProductService(int id, ProductRequest request)
        {
            var productEntity = _productRepository.GetProductByIdRepository(id);
            if (productEntity == null)
                {
                    throw new Exception("Producto no encontrado");
                }
            ProductProfile.ToProductUpdate(productEntity, request);
            _productRepository.UpdateProductRepository(productEntity);
            return ProductProfile.ToProductResponse(productEntity);
        }
        public ProductResponse DeleteProductService(int id)
        {
            var productEntity = _productRepository.GetProductByIdRepository(id);
            if (productEntity == null)
            {
                throw new Exception("Producto no encontrado");
            }
            _productRepository.DeleteProductRepository(productEntity);
            return ProductProfile.ToProductResponse(productEntity);
        }
    }
}

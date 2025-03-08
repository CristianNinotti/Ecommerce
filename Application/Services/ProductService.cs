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
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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
            // Obtener la categoría desde el repositorio usando CategoryId
            var category = _categoryRepository.GetCategoryById(product.CategoryId);

            // Si la categoría no existe, lanzar una excepción o manejar el error apropiadamente
            if (category == null)
            {
                throw new ArgumentException("Categoría no válida, debe crearla primero.");
            }

            // Mapear el request al objeto de entidad Product
            var productEntity = ProductProfile.ToProductEntity(product);

            // Asignar la categoría al producto
            productEntity.Categoria = category; // Aquí estamos asignando la categoría completa, no solo el ID

            // Crear el producto en el repositorio
            _productRepository.CreateProductRepository(productEntity);
        }
        public bool ToUpdateProduct(int id, ProductRequest request)
        {
            var productEntity = _productRepository.GetProductByIdRepository(id);
            if (productEntity == null)
            {
                return false;
            }
            // Obtener la categoría de la base de datos usando CategoryId
            var category = _categoryRepository.GetCategoryById(request.CategoryId);

            // Si la categoría no existe, lanzar una excepción o manejar el error apropiadamente
            if (category == null)
            {
                throw new ArgumentException("Categoría no válida, debe crearla primero.");
            }
            ProductProfile.ToProductUpdate(productEntity, request);
            // Asignar la categoría al producto actualizado
            productEntity.Categoria = category; // Aquí asignamos la categoría completa
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

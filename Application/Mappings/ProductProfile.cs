using System;
using Domain.Entities;
using Application.Models.Request;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Response;

namespace Application.Mappings
{
    public static class ProductProfile
    {
        public static Product ToProductEntity(ProductRequest product)
        {
            return new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Photos = product.Photos,
            };
        }
        public static ProductResponse ToProductResponse (Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Photos = product.Photos,
                CategoryId = product.CategoryId,
            };
        }
        public static void ToProductUpdate (Product product, ProductRequest request )
        {
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.Photos = request.Photos;
        }
    }
}

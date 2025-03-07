using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
    public static class OrderItemProfile
    {
        public static OrderItem ToOrderItemEntity(OrderItemRequest orderItem)
        {
            return new OrderItem()
            {

                OrderId = orderItem.OrderId, //ver
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,

            };
        }

        public static OrderItemResponse ToOrderItemResponse(OrderItem orderItem)
        {
            return new OrderItemResponse
            {
                Id = orderItem.Id, 
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                TotalPrice = orderItem.TotalPrice,

            };
        }

        public static List<OrderItemResponse> ToOrderItemResponse(List<OrderItem> orderItem)
        {
            return orderItem.Select(c => new OrderItemResponse
            {
                Id = c.Id,
                OrderId = c.OrderId,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Price = c.Price,
                TotalPrice = c.TotalPrice,
            }).ToList();
        }

        public static void ToOrderItemUpdate(OrderItem orderItem, OrderItemRequest request)
        {
            orderItem.OrderId = request.OrderId; // ver
            orderItem.ProductId = request.ProductId;
            orderItem.Quantity = request.Quantity;
            orderItem.Price = request.Price;
        }

    }
}

using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
    public class OrderProfile
    {
        public static Order ToOrderEntity(OrderRequest order)
        {
            return new Order()
            {
                OrderDate = DateTime.Now,
                OrderStatus = true,
                OrderItems = new List<OrderItem>(),
            };
        }

        // Método para convertir Order a OrderResponse
        public static OrderResponse ToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                UserId = order.UserId,
                OrderItems = OrderItemProfile.ToOrderItemResponse(order.OrderItems) // Reutilizamos el método ya existente
            };
        }

        // Método para convertir una lista de Orders a OrderResponse
        public static List<OrderResponse> ToOrderResponse(List<Order> orders)
        {
            return orders.Select(ToOrderResponse).ToList(); // Reutilizamos el método anterior
        }

        // Falta update

    }
}

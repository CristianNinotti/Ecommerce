using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
    public class OrderProfile
    {

        public static Order ToOrderEntity(OrderRequest orderRequest)
        {
            return new Order()
            {
                OrderDate = DateTime.Now,      // Asignamos la fecha de la orden
                OrderStatus = orderRequest.OrderStatus,            // Lo seteamos como true al crear la orden
                UserId = orderRequest.UserId,  // UserId del request
                                               // No tocamos la lista de OrderItems, se maneja fuera de la creación
            };
        }

        public static OrderResponse ToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                UserId = order.UserId,
                OrderItems = OrderItemProfile.ToOrderItemResponse(order.OrderItems) // Aquí mapeamos los OrderItems
            };
        }

        public static List<OrderResponse> ToOrderResponse(List<Order> orders)
        {
            return orders.Select(ToOrderResponse).ToList();
        }
        public static void ToOrderUpdate(Order order, OrderRequest request)
        {
            order.OrderStatus = request.OrderStatus;
        }
    }
}

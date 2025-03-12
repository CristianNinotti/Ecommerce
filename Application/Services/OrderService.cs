using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository; // Inicializamos el repositorio de orderItems
        }

        public List<OrderResponse> GetAllOrderItems()
        {
            var orders = _orderRepository.GetAllOrdersRepository();
            return OrderProfile.ToOrderResponse(orders);
        }
        public OrderResponse? GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderByIdRepository(id);
            if (order != null)
            {
                return OrderProfile.ToOrderResponse(order);
            }
            return null;
        }

        // Método para crear una orden con sus items
        public void CreateOrder(Order order)
        {
            decimal totalAmount = 0;

            // Calculamos el TotalAmount sumando los TotalPrice de los OrderItems
            foreach (var orderItem in order.OrderItems)
            {
                totalAmount += orderItem.TotalPrice;
            }

            // Asignamos el TotalAmount calculado
            order.TotalAmount = totalAmount;

            // Agregamos la orden a la base de datos
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Guardamos los OrderItems
            foreach (var orderItem in order.OrderItems)
            {
                _orderItemRepository.CreateOrderItemRepository(orderItem); // Delegamos la creación de OrderItem al repositorio
            }
        }
    }


}
}

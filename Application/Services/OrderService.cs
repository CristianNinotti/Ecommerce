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
        private readonly IProductRepository _productRepository;
        private readonly IMayoristaRepository _mayoristaRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, IMayoristaRepository mayoristaRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _mayoristaRepository = mayoristaRepository;
        }

        public List<OrderResponse> GetAllOrders()
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

        public void CreateOrder(OrderRequest orderRequest)
        {
            var order = OrderProfile.ToOrderEntity(orderRequest);
           // decimal totalAmount = 0; //= order.OrderItems.Sum(oi => oi.TotalPrice);
           // order.TotalAmount = totalAmount;
            _orderRepository.CreateOrderRepository(order);
        }

        public bool ToUpdateOrder(int userId, int orderId, OrderRequest request)
        {
            var orderEntity = _orderRepository.GetOrderByIdRepository(orderId);
            if (orderEntity == null || orderEntity.OrderStatus == false || userId != orderEntity.UserId)
            {
                return false; 
            }
            if (orderEntity.OrderItems.Any())
            {
                orderEntity.TotalAmount = orderEntity.OrderItems.Sum(oi => oi.TotalPrice);
                var user = _mayoristaRepository.GetMayoristaById(orderEntity.UserId);
                if (user != null)
                {
                    orderEntity.TotalAmount = orderEntity.TotalAmount * 0.9m;
                }
                _orderRepository.UpdateOrderRepository(orderEntity);
            }
            orderEntity.OrderStatus = request.OrderStatus;
            return true;
        }

        public bool DeleteOrder(int orderId)
        {
            var orderEntity = _orderRepository.GetOrderByIdRepository(orderId);
            if (orderEntity == null)
            {
                return false;
            }
            _orderRepository.DeleteOrderRepository(orderEntity);
            return true;
        }
    }
}


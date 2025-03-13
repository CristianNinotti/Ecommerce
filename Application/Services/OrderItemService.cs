using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Interfaces;

namespace Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public List<OrderItemResponse> GetAllOrderItems()
        {
            var orderItems = _orderItemRepository.GetAllOrderItemsRepository();
            return OrderItemProfile.ToOrderItemResponse(orderItems);
        }

        public OrderItemResponse? GetOrderItemById(int id)
        {
            var orderItem = _orderItemRepository.GetOrderItemByIdRepository(id);
            if (orderItem != null)
            {
                return OrderItemProfile.ToOrderItemResponse(orderItem);
            }
            return null;
        }

        public void CreateOrderItem(OrderItemRequest orderItem)
        {
            var product = _productRepository.GetProductByIdRepository(orderItem.ProductId);
            if (product == null)
            {
                throw new Exception("Producto no encontrado");
            }
            if (orderItem.Quantity > product.Stock)
            {
                throw new InvalidOperationException($"Stock insuficiente. Disponible: {product.Stock}");
            }
            var totalPrice = orderItem.Quantity * product.Price;
            var orderItemEntity = OrderItemProfile.ToOrderItemEntity(orderItem, product.Price);
            orderItemEntity.TotalPrice = totalPrice;
            _orderItemRepository.CreateOrderItemRepository(orderItemEntity);
        }

        public bool ToUpdateOrderItem(int userId, int orderItemId, OrderItemRequest request)
        {
            var orderEntity = _orderRepository.GetOrderByIdRepository(request.OrderId);
            var orderItemEntity = _orderItemRepository.GetOrderItemByIdRepository(orderItemId);
            var product = _productRepository.GetProductByIdRepository(request.ProductId);

            if (orderEntity == null ||orderItemEntity == null || userId != orderEntity.UserId || product == null)
            {
                return false;
            }
            var stockDisponible = product.Stock + orderItemEntity.Quantity;
            if (request.Quantity > stockDisponible)
            {
                throw new InvalidOperationException("Stock insuficiente para la cantidad solicitada.");
            }
            var totalPrice = request.Quantity * product.Price;
            OrderItemProfile.ToOrderItemUpdate(orderItemEntity, request, product.Price);
            orderItemEntity.TotalPrice = totalPrice;
            _orderItemRepository.UpdateOrderItemRepository(orderItemEntity);
            return true;
        }

        public bool DeleteOrderItem(int id)
        {
            var orderItemEntity = _orderItemRepository.GetOrderItemByIdRepository(id);
            if (orderItemEntity == null)
            {
                return false;
            }
            _orderItemRepository.DeleteOrderItemRepository(orderItemEntity);
            return true;
        }
    }
}

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
        private readonly IMayoristaRepository _mayoristaRepository;


        public OrderItemService(IOrderItemRepository orderItemRepository, IProductRepository productRepository, IOrderRepository orderRepository, IMayoristaRepository mayoristaRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mayoristaRepository = mayoristaRepository;
        }

        ///  Variable Global Discount

        private readonly decimal discount = 0.9m;

        public List<OrderItemResponse> GetAllOrderItems()
        {
            var orderItems = _orderItemRepository.GetAllOrderItemsRepository();
            return OrderItemProfile.ToOrderItemResponse(orderItems);
        }

        public List<OrderItemResponse> GetAllOrderItemsByProductId(int id)
        {
            var orderItems = _orderItemRepository.GetAllOrderItemsByProductIdRepository(id);
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
            var orderEntity = _orderRepository.GetOrderByIdRepository(orderItem.OrderId);
            if (product != null && orderEntity != null && orderEntity.OrderStatus == true && product.Available == true && orderItem.Quantity <= product.Stock)
            {
                var mayoristaEntity = _mayoristaRepository.GetMayoristaById(orderEntity.UserId);
                var totalPrice = orderItem.Quantity * product.Price;
                var orderItemEntity = OrderItemProfile.ToOrderItemEntity(orderItem, product.Price);
                orderItemEntity.TotalPrice = totalPrice;
                if (mayoristaEntity != null)
                {
                    orderItemEntity.TotalPrice = orderItemEntity.TotalPrice * discount;
                }
                _orderItemRepository.CreateOrderItemRepository(orderItemEntity);
                orderEntity.TotalAmount = _orderItemRepository.GetOrderItemsByOrderIdRepository(orderItem.OrderId).Where(oi => oi.Available == true).Sum(oi => oi.TotalPrice);
                _orderRepository.UpdateOrderRepository(orderEntity);
            }
        }

        public bool ToUpdateOrderItem(int userId, int orderItemId, OrderItemRequest request)
        {
            var orderEntity = _orderRepository.GetOrderByIdRepository(request.OrderId);
            var orderItemEntity = _orderItemRepository.GetOrderItemByIdRepository(orderItemId);
            var product = _productRepository.GetProductByIdRepository(request.ProductId);
            var mayoristaEntity = _mayoristaRepository.GetMayoristaById(userId);
            if (orderEntity != null && orderItemEntity != null && userId == orderEntity.UserId && product != null && orderEntity.OrderStatus == true && product.Available == true)
            {
                var stockDisponible = product.Stock + orderItemEntity.Quantity;
                if (request.Quantity <= stockDisponible)
                {
                    orderItemEntity.TotalPrice = request.Quantity * product.Price;
                    if (mayoristaEntity != null)
                    {
                        orderItemEntity.TotalPrice  *= discount;
                    }
                    OrderItemProfile.ToOrderItemUpdate(orderItemEntity, request, product.Price);
                    _orderItemRepository.UpdateOrderItemRepository(orderItemEntity);
                    orderEntity.TotalAmount = _orderItemRepository.GetOrderItemsByOrderIdRepository(orderEntity.Id).Where(oi => oi.Available == true).Sum(oi => oi.TotalPrice);
                    _orderRepository.UpdateOrderRepository(orderEntity);
                    return true;
                }
            }
            return false;
        }

        public bool SoftDeleteOrderItem(int id)
        {
            var orderItemEntity = _orderItemRepository.GetOrderItemByIdRepository(id);
            if (orderItemEntity == null)
            {
                return false;
            }
            orderItemEntity.Available = false;
            _orderItemRepository.UpdateOrderItemRepository(orderItemEntity);
            var orderEntity = _orderRepository.GetOrderByIdRepository(orderItemEntity.OrderId);
            if (orderEntity == null)
            {
                return false;
            }
            var updatedTotalAmount = _orderItemRepository.GetOrderItemsByOrderIdRepository(orderEntity.Id)
                .Where(oi => oi.Available)
                .Sum(oi => oi.TotalPrice);
            orderEntity.TotalAmount = updatedTotalAmount;
            _orderRepository.UpdateOrderRepository(orderEntity);
            return true;
        }

        public bool HardDeleteOrderItem(int id)
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

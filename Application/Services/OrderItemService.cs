using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository; // Usamos IProductRepository directamente

        public OrderItemService(IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository; // Inicializamos el repositorio de productos
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
            // Usamos el repositorio de productos para obtener el precio
            var product = _productRepository.GetProductByIdRepository(orderItem.ProductId); // Obtener el producto por su ID
            if (product == null)
            {
                throw new Exception("Producto no encontrado");
            }
            if (orderItem.Quantity > product.Stock)
            {
                throw new InvalidOperationException($"Stock insuficiente. Disponible: {product.Stock}");
            }

            // Calculamos el TotalPrice aquí antes de crear el OrderItem
            var totalPrice = orderItem.Quantity * product.Price;

            // Pasamos el precio del producto al crear el OrderItem
            var orderItemEntity = OrderItemProfile.ToOrderItemEntity(orderItem, product.Price);
            orderItemEntity.TotalPrice = totalPrice; // Asignar TotalPrice antes de guardar
            _orderItemRepository.CreateOrderItemRepository(orderItemEntity);
        }


        public bool ToUpdateOrderItem(int id, OrderItemRequest request)
        {
            var orderItemEntity = _orderItemRepository.GetOrderItemByIdRepository(id);
            if (orderItemEntity == null)
            {
                return false;
            }

            // Obtener el producto asociado para verificar stock y precio
            var product = _productRepository.GetProductByIdRepository(request.ProductId);
            if (product == null)
            {
                return false; // O lanzar una excepción si lo prefieres
            }

            // Calcular stock disponible (considerando el stock actual + lo que ya estaba reservado)
            var stockDisponible = product.Stock + orderItemEntity.Quantity;

            // Verificar si la cantidad solicitada supera el stock disponible
            if (request.Quantity > stockDisponible)
            {
                throw new InvalidOperationException("Stock insuficiente para la cantidad solicitada.");
            }

            // Calculamos el TotalPrice aquí con la nueva cantidad (request.Quantity)
            var totalPrice = request.Quantity * product.Price;

            // Pasamos la nueva información al método de actualización
            OrderItemProfile.ToOrderItemUpdate(orderItemEntity, request, product.Price);  // <-- Aca se pasa el precio del producto
            orderItemEntity.TotalPrice = totalPrice; // Asignar TotalPrice aquí
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

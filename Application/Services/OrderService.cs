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


        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository; // Inicializamos el repositorio de orderItems
            _productRepository = productRepository; // Inicializamos el repositorio de product tambien por el update.
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

        // Método para crear una orden con sus items
        public void CreateOrder(OrderRequest orderRequest)
        {
            // Convertimos el request en una entidad Order
            var order = OrderProfile.ToOrderEntity(orderRequest);

            // Guardamos la orden en la base de datos para obtener su ID
            _orderRepository.CreateOrderRepository(order);

            // Obtenemos los OrderItems asociados a esta orden (No en el individual)
            // Aca se usa el nuevo metodo con IEnumerable de OrderItemRepository de Id que recorre toda la lista
            var orderItems = _orderItemRepository.GetOrderItemsByOrderIdRepository(order.Id);

            // Sumamos los TotalPrice de los OrderItems ya existentes
            order.TotalAmount = orderItems.Sum(oi => oi.TotalPrice);

            // Actualizamos la orden con el TotalAmount calculado
            _orderRepository.UpdateOrderRepository(order);
        }

        public bool ToUpdateOrder(int orderId, OrderRequest request)
        {
            // Paso 1: Obtener la orden que queremos actualizar
            var orderEntity = _orderRepository.GetOrderByIdRepository(orderId);
            if (orderEntity == null)
            {
                return false; // Si no existe la orden, devolvemos false
            }

            // Paso 2: Verificar y actualizar los OrderItems (si es necesario)
            foreach (var orderItemRequest in request.OrderItems)
            {
                // Asegúrate de que orderItemRequest tiene la propiedad Id
                var orderItemEntity = orderEntity.OrderItems
                                                  .FirstOrDefault(oi => oi.Id == orderItemRequest.OrderId);

                if (orderItemEntity != null)
                {
                    // Paso 2.1: Verificar stock y recalcular el TotalPrice
                    var product = _productRepository.GetProductByIdRepository(orderItemRequest.ProductId);
                    if (product == null)
                    {
                        throw new InvalidOperationException("Producto no encontrado.");
                    }

                    // Calcular el stock disponible
                    var stockDisponible = product.Stock + orderItemEntity.Quantity;

                    if (orderItemRequest.Quantity > stockDisponible)
                    {
                        throw new InvalidOperationException("Stock insuficiente para la cantidad solicitada.");
                    }

                    // Paso 2.2: Actualizar la cantidad y el precio total
                    orderItemEntity.Quantity = orderItemRequest.Quantity;
                    orderItemEntity.TotalPrice = orderItemEntity.Quantity * product.Price;

                    // Guardamos el cambio en el repositorio de OrderItem
                    _orderItemRepository.UpdateOrderItemRepository(orderItemEntity);
                }
            }

            // Paso 3: Actualizar el TotalAmount de la orden, sumando el TotalPrice de todos los OrderItems
            orderEntity.TotalAmount = orderEntity.OrderItems.Sum(oi => oi.TotalPrice);

            // Paso 4: Actualizar la orden en la base de datos
            _orderRepository.UpdateOrderRepository(orderEntity);

            return true; // Si todo va bien, devolvemos true
        }


        public bool DeleteOrder(int orderId)
        {
            // Paso 1: Obtener la orden que queremos eliminar
            var orderEntity = _orderRepository.GetOrderByIdRepository(orderId);
            if (orderEntity == null)
            {
                return false; // Si no existe la orden, devolvemos false
            }

            // Paso 2: Eliminar la orden, los OrderItems deberían ser eliminados automáticamente
            _orderRepository.DeleteOrderRepository(orderEntity);

            return true; // Si todo va bien, devolvemos true
        }



    }


}


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
            _orderItemRepository = orderItemRepository; // Inicializamos el repositorio de orderItems
            _productRepository = productRepository; // Inicializamos el repositorio de product tambien por el update.
            _mayoristaRepository = mayoristaRepository; // Inicializamos el repositorio de mayorista para verificar si es el tipo de usuario y aplicar descuento.
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
            // Primero, creamos la orden
            var order = new Order
            {
                UserId = orderRequest.UserId,
                OrderDate = DateTime.Now, // Asignamos la fecha de la orden
                OrderStatus = orderRequest.OrderStatus
            };

            // Guardamos la orden en la base de datos
            _orderRepository.CreateOrderRepository(order);

            // Inicializamos una variable para el total de la orden
            decimal totalAmount = 0;

            // Ahora, creamos los OrderItems y les asignamos el OrderId correspondiente
            foreach (var orderItemRequest in orderRequest.OrderItems)
            {
                // Obtener el producto de la base de datos usando el ProductId
                var product = _productRepository.GetProductByIdRepository(orderItemRequest.ProductId);
                if (product == null)
                {
                    throw new Exception($"Producto con ID {orderItemRequest.ProductId} no encontrado.");
                }

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,  // Asignamos el OrderId de la orden recién creada
                    ProductId = orderItemRequest.ProductId,
                    Quantity = orderItemRequest.Quantity,
                    Price = product.Price,  // Asignamos el precio del producto desde la base de datos
                    TotalPrice = orderItemRequest.Quantity * product.Price  // Calculamos el TotalPrice
                };

                // Guardamos el OrderItem en la base de datos
                _orderItemRepository.CreateOrderItemRepository(orderItem);

                // Sumamos el TotalPrice de este OrderItem al totalAmount de la orden
                totalAmount += orderItem.TotalPrice;
            }

            // Ahora que tenemos el totalAmount, asignamos el valor a la orden
            order.TotalAmount = totalAmount;

            // Si el usuario es Mayorista, aplicamos el descuento del 10%
            var user = _mayoristaRepository.GetMayoristaById(orderRequest.UserId);
            if (user != null)
            {
                order.TotalAmount *= 0.90m; // Aplicamos el descuento
            }

            // Finalmente, actualizamos la orden con el total calculado
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
                // Asegúrate de que orderItemRequest tiene la propiedad Id correcta
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

            // Paso 3: Obtener el usuario de la orden para verificar si es Mayorista
            var user = _mayoristaRepository.GetMayoristaById(orderEntity.UserId);

            // Paso 4: Calcular el total de la orden
            var totalAmount = orderEntity.OrderItems.Sum(oi => oi.TotalPrice);

            // Si el usuario es Mayorista, aplicar el 10% de descuento
            orderEntity.TotalAmount = (user != null) ? totalAmount * 0.90m : totalAmount;

            // Paso 5: Actualizar la orden en la base de datos
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


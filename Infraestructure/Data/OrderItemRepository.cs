using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Context;

namespace Infrastructure.Data
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly EcommerceDbContext _orderItem;

        public OrderItemRepository(EcommerceDbContext orderItem)
        {
            _orderItem = orderItem;
        }

        public List<OrderItem> GetAllOrderItemsRepository()
        {
            return _orderItem.OrderItems.ToList();
        }

        public OrderItem? GetOrderItemByIdRepository(int id)
        {
            return _orderItem.OrderItems.FirstOrDefault(m => m.Id == id);
        }
        // Se utiliza en OrderService para poder utilizar la sumatoria y recorrido de la lista en el "Create"
        public IEnumerable<OrderItem> GetOrderItemsByOrderIdRepository(int orderId)
        {
            return _orderItem.OrderItems.Where(m => m.OrderId == orderId).ToList();
        }

        public void CreateOrderItemRepository(OrderItem orderItem) 
        {
            _orderItem.OrderItems.Add(orderItem);
            _orderItem.SaveChanges();
        }

        public void UpdateOrderItemRepository(OrderItem orderItem)
        {
            _orderItem.OrderItems.Update(orderItem);
            _orderItem.SaveChanges();
     
        }
        public void DeleteOrderItemRepository(OrderItem orderItem)
        {
            _orderItem.OrderItems.Remove(orderItem);
            _orderItem.SaveChanges();
        }
    }    
}

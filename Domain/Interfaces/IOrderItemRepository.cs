using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAllOrderItemsRepository();
        OrderItem? GetOrderItemByIdRepository(int id);
        IEnumerable<OrderItem> GetOrderItemsByOrderIdRepository(int orderId);
        void CreateOrderItemRepository(OrderItem orderItem);
        void UpdateOrderItemRepository(OrderItem orderItem);
        void DeleteOrderItemRepository(OrderItem orderItem);
    }
}

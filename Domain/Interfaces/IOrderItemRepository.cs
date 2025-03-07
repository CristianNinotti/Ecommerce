using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAllOrderItemsRepository();
        OrderItem? GetOrderItemByIdRepository(int id);
        void CreateOrderItemRepository(OrderItem orderItem);
        void UpdateOrderItemRepository(OrderItem orderItem);
        void DeleteOrderItemRepository(OrderItem orderItem);
    }
}

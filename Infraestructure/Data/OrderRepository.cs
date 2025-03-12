using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Context;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _order;

        public OrderRepository(EcommerceDbContext order)
        {
            _order = order;
        }

        public List<Order> GetAllOrdersRepository()
        {
            return _order.Orders.ToList();
        }

        public Order? GetOrderByIdRepository(int id)
        {
            return _order.Orders.FirstOrDefault(m => m.Id == id);
        }

        public void CreateOrderRepository(Order order)
        {
            _order.Orders.Add(order);
            _order.SaveChanges();
        }

        public void UpdateOrderRepository(Order order)
        {
            _order.Orders.Update(order);
            _order.SaveChanges();

        }

        public void DeleteOrderRepository(Order order)
        {
            _order.Orders.Remove(order);
            _order.SaveChanges();
        }
    
    }


}

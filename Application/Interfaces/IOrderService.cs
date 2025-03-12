using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        List<OrderResponse> GetAllOrders();
        OrderResponse? GetOrderById(int id);
        void CreateOrder(OrderRequest orderRequest);
        bool ToUpdateOrder(int orderId, OrderRequest request);
        bool DeleteOrder(int orderId);
    }
}

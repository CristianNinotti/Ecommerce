using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderItemService
    {
        List<OrderItemResponse> GetAllOrderItems();
        OrderItemResponse? GetOrderItemById(int id);
        void CreateOrderItem(OrderItemRequest orderItem);
        bool ToUpdateOrderItem(int id, OrderItemRequest request);
        bool DeleteOrderItem(int id);
    }
}

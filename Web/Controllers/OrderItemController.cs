using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("AllOrdersItems")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult GetAllOrderItems()
        {
            var orderItems = _orderItemService.GetAllOrderItems();
            return Ok(orderItems);
        }

        [HttpGet("OrderItemById/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult OrderItemById([FromRoute] int id)
        {
            try
            {
                var orderItem = _orderItemService.GetOrderItemById(id);
                return Ok(orderItem);
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }
        [HttpPost("CreateOrderItem")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult CreateOrderItem([FromBody] OrderItemRequest orderItem)
        {
            _orderItemService.CreateOrderItem(orderItem);
            return Ok("Orden Creada");
        }
        [HttpPut("UpdateOrderItem/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult UpdateOrderItem([FromRoute] int id, OrderItemRequest orderItem)
        {
            try
            {
                var UpdatedOrderItem = _orderItemService.ToUpdateOrderItem(id, orderItem);
                return Ok(UpdatedOrderItem);
            }
            catch (Exception ex)
            {
                return NotFound($"OrderItem con ID {id} no encontrada. Error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteOrderItem/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult DeleteOrderItem([FromRoute] int id)
        {
            try
            {
                _orderItemService.DeleteOrderItem(id);
                return Ok("Orden Eliminada");
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }
    }
}

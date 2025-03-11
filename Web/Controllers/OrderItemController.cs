using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// prueba git

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
            try
            {
                _orderItemService.CreateOrderItem(orderItem);
                return Ok("Orden Creada");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = "No hay stock suficiente para la cantidad solicitada. " + ex.Message }); // 400 Bad Request si no hay stock (Hace referencia a la validacion del servicio)
            }
        }
        [HttpPut("UpdateOrderItem/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult UpdateOrderItem([FromRoute] int id, OrderItemRequest orderItem)
        {
            try
            {
                var updateSuccess = _orderItemService.ToUpdateOrderItem(id, orderItem);

                // Verifica si la actualización fue exitosa
                if (updateSuccess)
                {
                    return Ok("OrderItem actualizado correctamente.");
                }
                else
                {
                    return NotFound($"OrderItem con ID {id} no encontrada.");
                }
            }
            catch (InvalidOperationException ex)
            {
                // Capturamos específicamente errores relacionados con la validación de stock
                return BadRequest($"No hay stock suficiente para la cantidad solicitada. Error: {ex.Message}");
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

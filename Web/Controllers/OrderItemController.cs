using Application.Interfaces;
using Application.Models.Request;
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

        [HttpGet("AllOrdersItemsAvailable")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult GetAllOrderItemsAvailable()
        {
            var orderItems = _orderItemService.GetAllOrderItems().Where(o=>o.Available);
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
        [HttpPut("UpdateOrderItem/{orderItemId}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult UpdateOrderItem([FromRoute]int orderItemId, [FromBody] OrderItemRequest orderItem)
        {

            try
            {
                var updateSuccess = _orderItemService.ToUpdateOrderItem(orderItemId, orderItem);

                // Verifica si la actualización fue exitosa
                if (updateSuccess)
                {
                    return Ok("OrderItem actualizado correctamente.");
                }
                else
                {
                    return NotFound($"Este ID {orderItemId} no corresponde a tu usuario.");
                }
            }
            catch (InvalidOperationException ex)
            {
                // Capturamos específicamente errores relacionados con la validación de stock
                return BadRequest($"No hay stock suficiente para la cantidad solicitada. Error: {ex.Message}");
            }
        }

        [HttpDelete("SoftDeleteOrderItem/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult SoftDeleteOrderItem([FromRoute] int id)
        {
            try
            {
                _orderItemService.SoftDeleteOrderItem(id);
                return Ok("Orden Eliminada");
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }

        [HttpDelete("HardDeleteOrderItem/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult HardDeleteOrderItem([FromRoute] int id)
        {
            try
            {
                _orderItemService.HardDeleteOrderItem(id);
                return Ok("Orden Eliminada");
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }
    }
}

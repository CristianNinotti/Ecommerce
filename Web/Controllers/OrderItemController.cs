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
            try
            {
                var orderItems = _orderItemService.GetAllOrderItems();
                if (!orderItems.Any())
                {
                    return BadRequest($"No hay orderItems registrados en el sistema");
                }
                return Ok(orderItems);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Se obtuvieron datos inesperados. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor. Error: {ex.Message}");
            }
        }

        [HttpGet("AllOrdersItemsAvailable")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult GetAllOrderItemsAvailable()
        {
            try
            {
                var orderItems = _orderItemService.GetAllOrderItems().Where(o => o.Available);
                if (!orderItems.Any())
                {
                    return BadRequest($"No se encontraron OrderItems habilitados en el sistema");
                }
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor. Error: {ex.Message}");
            }
        }

        [HttpGet("OrderItemById/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult OrderItemById([FromRoute] int id)
        {
            try
            {
                var orderItem = _orderItemService.GetOrderItemById(id);
                if (orderItem == null)
                {
                    return BadRequest($"No se encontro esa OrderItem");
                }
                return Ok(orderItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Se obtuvieron datos inesperados. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor. Error: {ex.Message}");
            }
        }
        [HttpPost("CreateOrderItem")]
        public IActionResult CreateOrderItem([FromBody] OrderItemRequest orderItem)
        {
            try
            {
                _orderItemService.CreateOrderItem(orderItem);
                return Ok("OrderItem Creado con exito");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"No se pudo crear el OrderItem");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Se obtuvieron datos inesperados. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor. Error: {ex.Message}");
            }
        }
        [HttpPut("UpdateOrderItem/{orderItemId}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult UpdateOrderItem([FromRoute] int orderItemId, [FromBody] OrderItemRequest orderItem)
        {

            try
            {
                var updateSuccess = _orderItemService.ToUpdateOrderItem(orderItemId, orderItem);

                if (!updateSuccess)
                {
                    return BadRequest($"No se pudo actualizar el OrderItem");
                }
                return Ok($"OrderItem actualizado con exito");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Se obtuvieron datos inesperados. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor. Error: {ex.Message}");
            }
        }

        [HttpDelete("SoftDeleteOrderItem/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult SoftDeleteOrderItem([FromRoute] int id)
        {
            try
            {
                var orderItem = _orderItemService.SoftDeleteOrderItem(id);
                if (!orderItem)
                {
                    return BadRequest($"No se pudo dar de baja el OrderItem");
                }
                return Ok("OrderItem dado de baja con exito");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Se obtuvieron datos inesperados. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor. Error: {ex.Message}");
            }
        }

        [HttpDelete("HardDeleteOrderItem/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult HardDeleteOrderItem([FromRoute] int id)
        {
            try
            {
                var orderItem = _orderItemService.HardDeleteOrderItem(id);
                if (!orderItem)
                {
                    return BadRequest($"No se pudo borrar el OrderItem");
                }
                return Ok("OrderItem borrado del sistema con exito");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Se obtuvieron datos inesperados. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor. Error: {ex.Message}");
            }
        }
    }
}
using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("AllOrders")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]

        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("OrderById/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]

        public IActionResult OrderById([FromRoute] int id)
        {
            try
            {
                var order = _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }

        [HttpPost("CreateOrder")]

        public IActionResult CreateOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                _orderService.CreateOrder(orderRequest);
                return Ok("Orden Creada");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = "Error al crear la orden. " + ex.Message });
            }
        }

        [HttpPut("UpdateOrder/{orderId}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]

        public IActionResult UpdateOrder([FromRoute] int orderId, [FromBody]  OrderRequest orderRequest)
        {
            string? userIdClaim = User.FindFirst("Id")?.Value;

            if (userIdClaim == null)
            {
                return BadRequest("No esta logueado");
            }
            int userId = int.Parse(userIdClaim);

            try
            {
                var updateSuccess = _orderService.ToUpdateOrder(userId, orderId , orderRequest);

                if (updateSuccess)
                {
                    return Ok("Orden actualizada correctamente.");
                }
                else
                {
                    return NotFound($"Orden con ID {orderId} no encontrada.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Error al actualizar la orden. Error: {ex.Message}");
            }
        }

        [HttpDelete("SoftDeleteOrder/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]

        public IActionResult SoftDeleteOrder([FromRoute] int id)
        {
            try
            {
                _orderService.SoftDeleteOrder(id);
                return Ok("Orden Eliminada");
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }

        [HttpDelete("HardDeleteOrder/{id}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]

        public IActionResult HardDeleteOrder([FromRoute] int id)
        {
            try
            {
                _orderService.HardDeleteOrder(id);
                return Ok("Orden Eliminada");
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }
    }
}

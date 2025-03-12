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

        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("OrderById/{id}")]
   
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

        [HttpPut("UpdateOrder/{id}")]

        public IActionResult UpdateOrder([FromRoute] int id, OrderRequest orderRequest)
        {
            try
            {
                var updateSuccess = _orderService.ToUpdateOrder(id, orderRequest);

                if (updateSuccess)
                {
                    return Ok("Orden actualizada correctamente.");
                }
                else
                {
                    return NotFound($"Orden con ID {id} no encontrada.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Error al actualizar la orden. Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteOrder/{id}")]
  
        public IActionResult DeleteOrder([FromRoute] int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return Ok("Orden Eliminada");
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {id} no encontrada. Error: {ex.Message}");
            }
        }
    }
}

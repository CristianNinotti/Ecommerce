using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("All Payments")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentService.GetAllPayments();
            return Ok(payments);
        }
        [HttpGet("PaymentById/{paymentId}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult GetByPayment([FromRoute]int paymentId)
        {
            try
            {
                var payment = _paymentService.GetPaymentById(paymentId);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return NotFound($"Orden con ID {paymentId} no encontrada. Error: {ex.Message}");
            }
        }

        [HttpPost("Create Payment")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult CreatePayment([FromBody] PaymentRequest request)
        {
            try
            {
                bool paymentCreated = _paymentService.CreatePayment(request);
                if (!paymentCreated)
                {
                    throw new InvalidOperationException("El pago no pudo ser creado.");
                }
                return Ok("Pago creado con éxito");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = "No se pudo crear el pago solicitado. " + ex.Message });
            }
        }
        [HttpPut("UpdatePayment/{paymentId}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult UpdatePayment([FromRoute] int paymentId, PaymentRequest request)
        {
            try
            {
                _paymentService.ToUpdatePayment(paymentId, request);
                return Ok("Payment Actualizado");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = "No se pudo actualizar el pago solicitado. " + ex.Message });
            }
        }

        [HttpDelete("DeletePayment/{paymentId}")]
        [Authorize(Policy = "MinoristaOrMayoristaOrSuperAdmin")]
        public IActionResult DeletePayment([FromRoute] int paymentId)
        {
            try
            {
                _paymentService.DeletePayment(paymentId);
                return Ok("Pago borrado con éxito");
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = "No se pudo borrar el pago solicitado. " + ex.Message });
            }
        }

    }
}

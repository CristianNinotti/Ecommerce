using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinoristaController : ControllerBase
    {
        private readonly IMinoristaService _minoristaService;
        public MinoristaController(IMinoristaService minoristaService)
        {
            _minoristaService = minoristaService;
        }
        [HttpGet("All Minorista")]
        public IActionResult GetAllMinoristas()
        {
            return Ok(_minoristaService.GetAllMinorista());

        }
        [HttpPost("Create Minorista")]
        public IActionResult CreateMinorista([FromBody] MinoristaRequest minorista)
        {
            _minoristaService.CreateMinorista(minorista);
            return Ok("Usuario Creado");
        }
        [HttpPut("UpdateMinorista/{id}")]
        public IActionResult UpdateMinorista([FromRoute] int id, MinoristaRequest minorista)
        {
            try
            {
                var updatedMinorista = _minoristaService.UpdateMinorista(id, minorista);
                return Ok(updatedMinorista);
            }
            catch (Exception ex)
            {
                return NotFound($"Minorista con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteMinorista/{id}")]
        public IActionResult DeleteMinorista([FromRoute] int id)
        {
            try
            {
                _minoristaService.DeleteMinorista(id);
                return Ok("Minorista Borrado");
            }
            catch (Exception ex)
            {
                return NotFound($"Minorista con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
    }
}

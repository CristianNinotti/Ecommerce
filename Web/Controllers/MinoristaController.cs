using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("All Minoristas")]
        [Authorize(Policy = "MinoristaOrSuperAdmin")]
        public IActionResult GetAllMinoristas()
        {
            return Ok(_minoristaService.GetAllMinorista());

        }

        [HttpGet("All Minoristas Available")]
        [Authorize(Policy = "MinoristaOrSuperAdmin")]
        public IActionResult GetAllMinoristasAvailable()
        {
            var minoristas = _minoristaService.GetAllMinorista().Where(o => o.Available);
            return Ok(minoristas);

        }
        [HttpPost("Create Minorista")]
        public IActionResult CreateMinorista([FromBody] MinoristaRequest minorista)
        {
            _minoristaService.CreateMinorista(minorista);
            return Ok("Usuario Creado");
        }
        [HttpPut("UpdateMinorista/{id}")]
        [Authorize(Policy = "MinoristaOrSuperAdmin")]
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
        [HttpDelete("SoftDeleteMinorista/{id}")]
        [Authorize(Policy = "MinoristaOrSuperAdmin")]
        public IActionResult SoftDeleteMinorista([FromRoute] int id)
        {
            try
            {
                var result = _minoristaService.SoftDeleteMinorista(id);
                if (!result)
                {
                    throw new InvalidOperationException($"Minorista con ID {id} no encontrado.");
                }
                return Ok("Minorista deshabilitado correctamente.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

        [HttpDelete("HardDeleteMinorista/{id}")]
        [Authorize(Policy = "MinoristaOrSuperAdmin")]
        public IActionResult HardDeleteMinorista([FromRoute] int id)
        {
            try
            {
                _minoristaService.HardDeleteMinorista(id);
                return Ok("Minorista Borrado");
            }
            catch (Exception ex)
            {
                return NotFound($"Minorista con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
    }
}

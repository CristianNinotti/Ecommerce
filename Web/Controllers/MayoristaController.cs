using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class MayoristaController : ControllerBase
    {
        private readonly IMayoristaService _mayoristaService;
        public MayoristaController(IMayoristaService mayoristaService)
        {
            _mayoristaService = mayoristaService;
        }

        [HttpGet("All Mayoristas")]
        [Authorize(Policy = "MayoristaOrSuperAdmin")]
        public IActionResult GetAllMayoristas()
        {
            return Ok(_mayoristaService.GetAllMayoristas());
        }


        [HttpGet("All Mayoristas Available")]
        [Authorize(Policy = "MayoristaOrSuperAdmin")]
        public IActionResult GetAllMayoristasAvailable()
        {
            var mayoristas = _mayoristaService.GetAllMayoristas().Where(o => o.Available);
            return Ok(mayoristas);
        }

        [HttpPost("CreateMayorista")]
        public IActionResult CreateMayorista([FromBody] MayoristaRequest mayorista)
        {
            try
            {
                _mayoristaService.CreateMayorista(mayorista);
                return Ok("Mayorista Creado");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateMayorista/{id}")]
        [Authorize(Policy = "MayoristaOrSuperAdmin")]
        public IActionResult UpdateMayorista([FromRoute] int id, MayoristaRequest mayorista)
        {
            try
            {
                var MayoristaUpdated = _mayoristaService.UpdateMayorista(id, mayorista);
                return Ok(MayoristaUpdated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = $"Mayorista con ID {id} no encontrado. Error: {ex.Message}" }); // Específico para no encontrado
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Mensaje claro si el nombre de cuenta o email ya están en uso
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error inesperado", detail = ex.Message }); // Captura de errores generales
            }
        }


        [HttpDelete("SoftDelete/{id}")]
        [Authorize(Policy = "MayoristaOrSuperAdmin")]
        public IActionResult SoftDeleteMayorista([FromRoute]int id)
        {
            try
            {
                _mayoristaService.SoftDeleteMayorista(id);
                return Ok("Mayorista Borrado");
            }
            catch (Exception ex)
            {
                return NotFound($"Mayorista con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
        [HttpDelete("HardDelete/{id}")]
        [Authorize(Policy = "MayoristaOrSuperAdmin")]
        public IActionResult HardDeleteMayorista([FromRoute] int id)
        {
            try
            {
                _mayoristaService.HardDeleteMayorista(id);
                return Ok("Mayorista Borrado");
            }
            catch (Exception ex)
            {
                return NotFound($"Mayorista con ID {id} no encontrado. Error: {ex.Message}");
            }
        }
    }
}

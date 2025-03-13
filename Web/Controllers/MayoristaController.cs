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

        [HttpPost("Create Mayorista")]
        public IActionResult CreateMayorista([FromBody] MayoristaRequest mayorista)
        {
            _mayoristaService.CreateMayorista(mayorista);
            return Ok("Mayorista Creado");
        }

        [HttpPut("UpdateMayorista/{id}")]
        [Authorize(Policy = "MayoristaOrSuperAdmin")]

        public IActionResult UpdateMayorista([FromRoute]int id, MayoristaRequest mayorista)
        {
            try
            {
                var MayoristaUpdated = _mayoristaService.UpdateMayorista(id, mayorista);
                return Ok(MayoristaUpdated);
            }
            catch (Exception ex)
            {
                return NotFound($"Mayorista con ID {id} no encontrado. Error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "MayoristaOrSuperAdmin")]
        public IActionResult DeleteMayorista([FromRoute]int id)
        {
            try
            {
                _mayoristaService.DeleteMayorista(id);
                return Ok("Mayorista Borrado");
            }
            catch (Exception ex)
            {
                return NotFound($"Mayorista con ID {id} no encontrado. Error: {ex.Message}");
            }
        }   
    }
}

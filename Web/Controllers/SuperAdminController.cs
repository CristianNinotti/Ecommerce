using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/superAdmin")]
[ApiController]
public class SuperAdminController : ControllerBase
{
    private readonly ISuperAdminService _superAdminService;

    public SuperAdminController(ISuperAdminService superAdminService)
    {
        _superAdminService = superAdminService;
    }

    [HttpGet("All SuperAdmins")]
    [Authorize(Policy = "SuperAdminOnly")]
    public IActionResult GetAllSuperAdmin()
    {
        var response = _superAdminService.GetAllSuperAdmins();

        if (!response.Any()) // Cambio aquí: !response.Any() en lugar de response.Any()
        {
            return NotFound("SuperAdmins not found");
        }

        return Ok(response);
    }

    [HttpGet("All SuperAdmins Available")]
    [Authorize(Policy = "SuperAdminOnly")]
    public IActionResult GetAllSuperAdminAvailable()
    {
        var response = _superAdminService.GetAllSuperAdmins().Where(o => o.Available);

        if (!response.Any()) // Cambio aquí también
        {
            return NotFound("SuperAdmins not found");
        }

        return Ok(response);
    }


    [HttpGet("{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public ActionResult<SuperAdminResponse?> GetSuperAdminById([FromRoute] int id)
    {
        var response = _superAdminService.GetSuperAdminById(id);

        if (response is null)
        {
            return NotFound("SuperAdmin not found");
        }

        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateSuperAdmin([FromBody] SuperAdminRequest superAdmin)
    {
        try 
        { 
        _superAdminService.CreateSuperAdmin(superAdmin);
        return Ok("Usuario Creado");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public ActionResult<bool> UpdateSuperAdmin([FromRoute] int id, [FromBody] SuperAdminRequest superAdmin)
    {
        try
        {
            var SuperAdminUpdated = _superAdminService.UpdateSuperAdmin(id, superAdmin);
            return Ok(SuperAdminUpdated);
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

    [HttpDelete("SoftDeleteAdmin/{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public ActionResult<bool> SoftDeleteSuperAdmin([FromRoute] int id)
    {
        return Ok(_superAdminService.SoftDeleteSuperAdmin(id));
    }

    [HttpDelete("HardDeleteAdmin/{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public ActionResult<bool> HardDeleteSuperAdmin([FromRoute] int id)
    {
        return Ok(_superAdminService.HardDeleteSuperAdmin(id));
    }
}
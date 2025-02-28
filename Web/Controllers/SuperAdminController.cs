using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
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

    [HttpGet]
    [Authorize(Policy = "SuperAdminOnly")]
    public IActionResult GetAllSuperAdmin()
    {
        var response = _superAdminService.GetAllSuperAdmins();

        if (response.Count is 0)
        {
            return NotFound("SuperAdmin not found");
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
        _superAdminService.CreateSuperAdmin(superAdmin);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public ActionResult<bool> UpdateSuperAdmin([FromRoute] int id, [FromBody] SuperAdminRequest superAdmin)
    {
        return Ok(_superAdminService.UpdateSuperAdmin(id, superAdmin));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public ActionResult<bool> DeleteSuperAdmin([FromRoute] int id)
    {
        return Ok(_superAdminService.DeleteSuperAdmin(id));
    }
}
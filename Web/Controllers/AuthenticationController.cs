using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _customAuthenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _customAuthenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                // Intenta autenticar y generar el token
                string token = _customAuthenticationService.Authenticate(authenticationRequest);
                return Ok(token);
            }
            catch (UnauthorizedAccessException) // Captura la excepción de autenticación fallida
            {
                // Devuelve un estado 401 Unauthorized con un mensaje claro
                return Unauthorized(new { message = "Credenciales incorrectas. Por favor, inténtelo de nuevo." });
            }
            catch (Exception ex)
            {
                // Captura otros errores si hay algún problema interno
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }
    }
}
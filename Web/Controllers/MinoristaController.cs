using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        [HttpGet("Minorista")]

        public IActionResult GetAllMinoristas()
        {
            return Ok(_minoristaService.GetAllMinorista());

        }
    }
}

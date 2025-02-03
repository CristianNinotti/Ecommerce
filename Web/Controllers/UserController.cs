using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("User/{id}")]

        public IActionResult Get()
        {
            return Ok(new List<User>
            {
                new() {
                    Id=1, 
                    FirstName="Facundo", 
                    LastName="Solari", 
                    NameAccount="facu", 
                    Password="facu123", 
                    Email="facu@hotmail.com",
                    PhoneNumber="+543413500300",
                    Address="Santafe 1234",
                },
                new()
                {
                    Id=2,
                    FirstName="Cristian",
                    LastName="Ninotti",
                    NameAccount="cris",
                    Password="cris123",
                    Email="cris@hotmail.com",
                    PhoneNumber="+543415155611",
                    Address="San Lorenzo 3624",
                }
            });

        }
    }
}

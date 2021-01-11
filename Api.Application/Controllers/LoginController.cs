using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] UserEntity userEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest("Login invalid!");

            if (userEntity == null)
                return BadRequest("Login invalid!");

            try
            {
                var result = await _service.FindByLogin(userEntity);
                if (result == null)
                    return NotFound("User not found");
                else
                    return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
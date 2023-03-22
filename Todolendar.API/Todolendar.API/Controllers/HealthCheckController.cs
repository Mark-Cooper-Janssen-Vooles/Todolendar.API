using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolendar.API.Models.DTO.Auth;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Controllers
{
    [ApiController]
    [Route("Ping")]
    public class HealthCheckController : ControllerBase
    {
        public HealthCheckController() {}

        [HttpGet]
        public async Task<IActionResult> healthCheck()
        {
            return Ok();
        }
    }
}

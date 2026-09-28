using HR.Service.Services;
using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICurrentUserService currentUserService;

        public TestController(ICurrentUserService currentUserService)
        {
            this.currentUserService = currentUserService;
        }
        [Authorize]
        [HttpGet("Me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = currentUserService.UserId,
                EmployeeId = currentUserService.EmployeeId,
                Email = currentUserService.Email,
                Roles = currentUserService.Roles,
                IsAuthenticated = currentUserService.IsAuthenticated
            });
        }
    }
}

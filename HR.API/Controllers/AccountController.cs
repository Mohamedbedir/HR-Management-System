using HR.Core.Bases;
using HR.Core.Features.Authantications.Commands.Models;
using HR.Data.AppMetaData;
using HR.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class AccountController : AppControllerBase
    {
        [HttpPost(Router.AccountRouting.LogIn)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> Login([FromBody] LoginCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        [HttpPost(Router.AccountRouting.Register)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> Register([FromBody] RegisterCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        
    }
}

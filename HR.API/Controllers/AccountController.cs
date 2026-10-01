using HR.Core.Bases;
using HR.Core.Features.Authantications.Commands.Models;
using HR.Core.Features.Authantications.Commands.Respnses;
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
        [ProducesResponseType(typeof(Response<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<LoginResponse>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<LoginResponse>>> Login([FromBody] LoginCommand model)
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
        //[Authorize]
        [HttpPost(Router.AccountRouting.RefreshToken)]
        [ProducesResponseType(typeof(Response<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<LoginResponse>), StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<Response<LoginResponse>>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await mediator.Send(command);

            return NewResult(response);
        }
        [Authorize]
        [HttpPost(Router.AccountRouting.LogOut)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> Logout([FromBody] LogoutCommand command)
        {
            var response = await mediator.Send(command);

            return NewResult(response);
        }

    }
}

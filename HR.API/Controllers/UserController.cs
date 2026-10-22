using HR.Core.Bases;
using HR.Core.Features.Applications.Queries.Models;
using HR.Core.Features.Applications.Queries.Responses;
using HR.Core.Features.Users.Commands.Models;
using HR.Core.Features.Users.Queries.Models;
using HR.Core.Features.Users.Queries.Resposes;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class UserController : AppControllerBase
    {
        //[Authorize(Roles = $"{Roles.Admin},{Roles.HR},{Roles.Recruiter}")]
        [HttpGet(Router.UserRouting.List)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllUsersResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetAllUsersResponse>>>> GetUsersList([FromQuery] string? Searsh)
        {
            var response = await mediator.Send(new GetAllUsersQuery() {searsh=Searsh });
            return NewResult(response);
        }

        [HttpPut(Router.UserRouting.LockUser)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> LockUser([FromRoute] string id)
        {

            var response = await mediator.Send(new LockUserCommand(id));
            return NewResult(response);
        }
        [HttpPut(Router.UserRouting.UnLockUser)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UnLockUser([FromRoute] string id)
        {

            var response = await mediator.Send(new UnLockUserCommand(id));
            return NewResult(response);
        }

        /*
        [HttpGet(Router.UserRouting.CurrentUser)]
        [ProducesResponseType(typeof(Response<GetCurrentUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest<GetCurrentUserResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetCurrentUserResponse>>> GetCurrentUser()
        {
            var response = await mediator.Send(new GetCurrentUserQuery());
            return NewResult(response);
        }
  
        [HttpPut(Router.UserRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdateUser([FromBody] EditAppUserCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        [HttpPut(Router.UserRouting.ChangePassword)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> ChangePassword([FromBody] ChangeUserPasswordCommand model)
        {

            var response = await mediator.Send(model);
            return NewResult(response);
        }
       



        [HttpDelete(Router.UserRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteUser([FromRoute] string id)
        {
            var response = await mediator.Send(new DeleteAppUserCommand(id));
            return NewResult(response);
        }*/
    }
}

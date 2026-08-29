using HR.Core.Bases;
using HR.Core.Features.LeaveTypes.Commands.Models;
using HR.Core.Features.LeaveTypes.Queries.Models;
using HR.Core.Features.LeaveTypes.Queries.Responses;
using HR.Core.Features.Positions.Commands.Models;
using HR.Core.Features.Positions.Queries.Models;
using HR.Core.Features.Positions.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class LeaveTypeController : AppControllerBase
    {
        [HttpGet(Router.LeaveTypeRouting.ById)]
        [ProducesResponseType(typeof(Response<GetLeaveTypeByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetLeaveTypeByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetLeaveTypeByIdResponse>>> GetLeaveTypeById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetLeaveTypeByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.LeaveTypeRouting.List)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetLeaveTypesResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetLeaveTypesResponse>>>> GetLeaveTypeList()
        {
            var response = await mediator.Send(new GetLeaveTypesQuery());
            return NewResult(response);
        }


        [HttpPost(Router.LeaveTypeRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> CreateLeaveType([FromBody] AddLeaveTypeCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }


        [HttpPut(Router.LeaveTypeRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdateLeaveType([FromRoute] int id,
            [FromBody] EditLeaveTypeCommand model)
        {
            model.Id = id;
            var response = await mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.LeaveTypeRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteLeaveType([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteLeaveTypeCommand(id));
            return NewResult(response);
        }
    }
}

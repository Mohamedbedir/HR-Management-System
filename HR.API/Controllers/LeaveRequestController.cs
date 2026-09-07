using HR.Core.Bases;
using HR.Core.Features.Attendances.Commands.Models;
using HR.Core.Features.Attendances.Queries.Models;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.LeaveRequests.Commands.Models;
using HR.Core.Features.LeaveRequests.Queries.Models;
using HR.Core.Features.LeaveRequests.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class LeaveRequestController : AppControllerBase
    {

        [HttpGet(Router.LeaveRequestRouting.ById)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<string>>> GetLeaveRequestById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetLeaveRequestByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.LeaveRequestRouting.ForEmployee)]
        [ProducesResponseType(typeof(Response<List<GetLeaveRequestsForEmployeeRespose>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetLeaveRequestsForEmployeeRespose>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetLeaveRequestsForEmployeeRespose>>>>
            GetLeaveRequestsForEmpliyee([FromRoute] int EmployeeId)
        {
            var response = await mediator.Send(new GetLeaveRequestsForEmployeeQuery(EmployeeId));
            return NewResult(response);
        }
        [HttpGet(Router.LeaveRequestRouting.List)]
        [ProducesResponseType(typeof(Response<List<GetLeaveRequestsRespose>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<List<GetLeaveRequestsRespose>>>>
            GetLeaveRequestsList()
        {
            var response = await mediator.Send(new GetLeaveRequestsQuery());
            return NewResult(response);
        }
        [HttpPost(Router.LeaveRequestRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Response<string>>> CreateLeaveRequest([FromBody] CreateLeaveRequestCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.LeaveRequestRouting.Approve)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> LeaveRequestApprove([FromBody] int LeaveRequestId)
        {
            var response = await mediator.Send(new ApproveLeaveRequestCommand(LeaveRequestId));
            return NewResult(response);
        }
        [HttpPut(Router.LeaveRequestRouting.Reject)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> LeaveRequestReject(
            [FromBody] RejectLeaveRequestCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.LeaveRequestRouting.Cancel)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> LeaveRequestCancel(
            [FromBody] CancelLeaveRequestCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}

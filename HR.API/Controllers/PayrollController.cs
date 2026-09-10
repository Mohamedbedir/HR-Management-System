using HR.Core.Bases;
using HR.Core.Features.LeaveRequests.Commands.Models;
using HR.Core.Features.LeaveRequests.Queries.Models;
using HR.Core.Features.LeaveRequests.Queries.Responses;
using HR.Core.Features.LeaveTypes.Queries.Models;
using HR.Core.Features.LeaveTypes.Queries.Responses;
using HR.Core.Features.Payrolls.Commands.Models;
using HR.Core.Features.Payrolls.Queries.Models;
using HR.Core.Features.Payrolls.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class PayrollController : AppControllerBase
    {
        [HttpGet(Router.PayrollRouting.ById)]
        [ProducesResponseType(typeof(Response<GetPayrollByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetPayrollByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetPayrollByIdResponse>>> GetPayrollById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetPayrollByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.PayrollRouting.List)]
        [ProducesResponseType(typeof(Response<List<GetPayrollsResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<GetPayrollByIdResponse>>> GetPayrolls()
        {
            var response = await mediator.Send(new GetPayrollsQuery());
            return NewResult(response);
        }
        [HttpGet(Router.PayrollRouting.ForEmployee)]
        [ProducesResponseType(typeof(Response<List<GetPayrollsForEmployeeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetPayrollsForEmployeeResponse>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetPayrollsForEmployeeResponse>>>>
            GetPayrollsForEmpliyee([FromRoute] int EmployeeId)
        {
            var response = await mediator.Send(new GetPayrollsForEmployeeQuery(EmployeeId));
            return NewResult(response);
        }
        [HttpPost(Router.PayrollRouting.Calculate)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Response<string>>> CalculatePayroll([FromBody] CalculatePayrollCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.PayrollRouting.Approve)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> ApprovePayroll([FromBody] ApprovePayrollCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.PayrollRouting.Pay)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> PayPayroll(
            [FromBody] PayPayrollCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.PayrollRouting.Cancel)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> CancelPayroll(
            [FromBody] CancelPayrollCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}

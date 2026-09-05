using HR.Core.Bases;
using HR.Core.Features.Attendances.Commands.Models;
using HR.Core.Features.Attendances.Queries.Models;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.EmployeeDocuments.Commands.Models;
using HR.Core.Features.EmployeeDocuments.Queries.Models;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class AttendanceController : AppControllerBase
    {
        [HttpPost(Router.AttendanceRouting.CheckIn)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Response<string>>> CheckIn([FromBody] CheckInCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.AttendanceRouting.CheckOut)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Response<string>>> CheckOut([FromBody] int employeeId)
        {
            var response = await mediator.Send(new CheckOutCommand(employeeId));
            return NewResult(response);
        }
        [HttpGet(Router.AttendanceRouting.ForEmployee)]
        [ProducesResponseType(typeof(Response<List<GetAttendancesForEmpliyeeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetAttendancesForEmpliyeeResponse>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetAttendancesForEmpliyeeResponse>>>> 
            GetAttendancesForEmpliyee([FromRoute] int EmployeeId)
        {
            var response = await mediator.Send(new GetAttendancesForEmpliyeeQuery(EmployeeId));
            return NewResult(response);
        }
        [HttpGet(Router.AttendanceRouting.ForEmployeeByDate)]
        [ProducesResponseType(typeof(Response<List<GetAttendancesForEmpliyeeByDateResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetAttendancesForEmpliyeeByDateResponse>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetAttendancesForEmpliyeeByDateResponse>>>> 
            GetAttendancesForEmpliyeeByDate([FromRoute] int EmployeeId,
            [FromQuery] DateOnly FromDate,[FromQuery] DateOnly ToDate)
        {
            var response = await mediator.Send(new GetAttendancesForEmpliyeeByDateQuery(EmployeeId,FromDate,ToDate));
            return NewResult(response);
        }
    }
}

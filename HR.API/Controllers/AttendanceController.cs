using HR.Core.Bases;
using HR.Core.Features.Attendances.Commands.Models;
using HR.Core.Features.Attendances.Queries.Models;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.EmployeeDocuments.Commands.Models;
using HR.Core.Features.EmployeeDocuments.Queries.Models;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Data.AppMetaData;
using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HR.API.Controllers
{
    [ApiController]
    public class AttendanceController : AppControllerBase
    {
        private readonly ICurrentUserService currentUserService;

        public AttendanceController(ICurrentUserService currentUserService)
        {
            this.currentUserService = currentUserService;
        }
        [Authorize(Roles = $"{Roles.Admin},{Roles.HR},{Roles.Manager},{Roles.Employee}")]
        [HttpPost(Router.AttendanceRouting.CheckIn)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Response<string>>> CheckIn([FromBody] CheckInCommand command)
        {
            // If EmployeeId is not supplied by client, get it from the current user service
            if (command.EmployeeId <= 0 && currentUserService.EmployeeId.HasValue)
            {
                command.EmployeeId = currentUserService.EmployeeId.Value;
            }
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Roles = $"{Roles.Admin},{Roles.HR},{Roles.Manager},{Roles.Employee}")]
        [HttpPut(Router.AttendanceRouting.CheckOut)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Response<string>>> CheckOut()
        {
            var response = await mediator.Send(new CheckOutCommand());
            return NewResult(response);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.HR},{Roles.Manager},{Roles.Employee}")]
        [HttpGet(Router.AttendanceRouting.ForEmployee)]
        [ProducesResponseType(typeof(Response<List<GetAttendancesForEmpliyeeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetAttendancesForEmpliyeeResponse>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetAttendancesForEmpliyeeResponse>>>> 
            GetAttendancesForEmpliyee([FromQuery] int EmployeeId)
        {
            if (EmployeeId <= 0 && currentUserService.EmployeeId.HasValue)
            {
                EmployeeId = currentUserService.EmployeeId.Value;
            }
            var response = await mediator.Send(new GetAttendancesForEmpliyeeQuery(EmployeeId));
            return NewResult(response);
        }
        [Authorize(Roles = $"{Roles.Admin},{Roles.HR},{Roles.Manager},{Roles.Employee}")]
        [HttpGet(Router.AttendanceRouting.ForEmployeeByDate)]
        [ProducesResponseType(typeof(Response<List<GetAttendancesForEmpliyeeByDateResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetAttendancesForEmpliyeeByDateResponse>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetAttendancesForEmpliyeeByDateResponse>>>> 
            GetAttendancesForEmpliyeeByDate([FromQuery] int EmployeeId,
            [FromQuery] DateOnly? FromDate,[FromQuery] DateOnly? ToDate)
        {
            if (EmployeeId <= 0 && currentUserService.EmployeeId.HasValue)
            {
                EmployeeId = currentUserService.EmployeeId.Value;
            }
            var defaultDate = DateOnly.FromDateTime(System.DateTime.Now);
            var from = FromDate ?? defaultDate;
            var to = ToDate ?? defaultDate;

            var response = await mediator.Send(new GetAttendancesForEmpliyeeByDateQuery(EmployeeId,from,to));
            return NewResult(response);
        }
    }
}

using HR.Core.Bases;
using HR.Core.Features.LeaveRequests.Commands.Models;
using HR.Core.Features.LeaveRequests.Queries.Models;
using HR.Core.Features.LeaveRequests.Queries.Responses;
using HR.Core.Features.PerformanceReviews.Commands.Models;
using HR.Core.Features.PerformanceReviews.Queries.Models;
using HR.Core.Features.PerformanceReviews.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class PerformanceReviewController : AppControllerBase
    {
        [HttpGet(Router.PerformanceReviewRouting.ById)]
        [ProducesResponseType(typeof(Response<GetReviewByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetReviewByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetReviewByIdResponse>>> GetReviewById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetReviewByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.PerformanceReviewRouting.ForEmployee)]
        [ProducesResponseType(typeof(Response<List<GetReviewsForEmployeeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetReviewsForEmployeeResponse>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetReviewsForEmployeeResponse>>>>
            GetReviewsForEmpliyee([FromRoute] int EmployeeId)
        {
            var response = await mediator.Send(new GetReviewsForEmployeeQuery(EmployeeId));
            return NewResult(response);
        }
        [HttpGet(Router.PerformanceReviewRouting.List)]
        [ProducesResponseType(typeof(Response<List<GetReviewsResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<List<GetReviewsResponse>>>>
            GetReviewsList()
        {
            var response = await mediator.Send(new GetReviewsQuery());
            return NewResult(response);
        }
        [HttpPost(Router.PerformanceReviewRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Response<string>>> CreateLeaveRequest([FromBody] CreatePerformanceReviewCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}

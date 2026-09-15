using HR.Core.Bases;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.Departments.Queries.Models;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.JobPostings.Commands.Models;
using HR.Core.Features.JobPostings.Queries.Models;
using HR.Core.Features.JobPostings.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class JobPostingController : AppControllerBase
    {
        [HttpGet(Router.JobPostingRouting.ById)]
        [ProducesResponseType(typeof(Response<GetJobPostingByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetDepartmentByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetJobPostingByIdResponse>>> GetJobPostingById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetJobPostingByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.JobPostingRouting.List)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetJobPostingsResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetJobPostingsResponse>>>> GetJobPostingsList()
        {
            var response = await mediator.Send(new GetJobPostingsQuery());
            return NewResult(response);
        }

        [HttpPost(Router.JobPostingRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> CreateJobPosting([FromBody] AddJobPostingCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        [HttpPut(Router.JobPostingRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdateJobPosting([FromRoute] int id,
           [FromBody] EditJobPostingCommand model)
        {
            model.Id = id;
            var response = await mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.JobPostingRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteJobPosting([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteJobPostingCommand(id));
            return NewResult(response);
        }
    }
}

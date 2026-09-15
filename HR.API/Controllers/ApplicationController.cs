using HR.Core.Bases;
using HR.Core.Features.Applications.Commands.Models;
using HR.Core.Features.Applications.Queries.Models;
using HR.Core.Features.Applications.Queries.Responses;
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
    public class ApplicationController : AppControllerBase
    {
        [HttpGet(Router.ApplicationRouting.ById)]
        [ProducesResponseType(typeof(Response<GetApplicationByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetApplicationByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetApplicationByIdResponse>>> GetApplicationById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetApplicationByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.ApplicationRouting.List)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetApplicationsResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetApplicationsResponse>>>> GetApplicationsList()
        {
            var response = await mediator.Send(new GetApplicationsQuery());
            return NewResult(response);
        }

        [HttpPost(Router.ApplicationRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> CreateApplication([FromBody] AddApplicationCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        [HttpPut(Router.ApplicationRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdateApplication([FromRoute] int id,
           [FromBody] EditApplicationCommand model)
        {
            model.Id = id;
            var response = await mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.ApplicationRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteApplication([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteApplicationCommand(id));
            return NewResult(response);
        }
    }
}

using HR.Core.Bases;
using HR.Core.Features.Candidates.Commands.Models;
using HR.Core.Features.Candidates.Queries.Models;
using HR.Core.Features.Candidates.Queries.Responses;
using HR.Core.Features.EmployeeDocuments.Commands.Models;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class CandidateController : AppControllerBase
    {
        [HttpGet(Router.CandidateRouting.ById)]
        [ProducesResponseType(typeof(Response<GetCandidateByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetCandidateByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetCandidateByIdResponse>>> GetCandidateById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetCandidateByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.CandidateRouting.List)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetCandidatesResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetCandidatesResponse>>>> GetCandidatesList()
        {
            var response = await mediator.Send(new GetCandidatesQuery());
            return NewResult(response);
        }
        [HttpPost(Router.CandidateRouting.Create)]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> Create([FromForm] AddCandidateCommand model)
        {
            var response = await mediator.Send(model);

            return NewResult(response);
        }

        [HttpPut(Router.CandidateRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdateCandidate([FromRoute] int id,
            [FromForm] EditCandidateCommand model)
        {
            model.Id = id;
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        [HttpDelete(Router.CandidateRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteCandidate([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteCandidateCommand(id));
            return NewResult(response);
        }
    }
}

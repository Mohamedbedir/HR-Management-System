using HR.Core.Bases;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.Departments.Queries.Models;
using HR.Core.Features.Departments.Queries.Responses;
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
    //[Route("api/[controller]")]
    [ApiController]
    public class PositionController : AppControllerBase
    {
        [HttpGet(Router.PositionRouting.ById)]
        [ProducesResponseType(typeof(Response<GetPositionByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetPositionByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetPositionByIdResponse>>> GetPositionById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetPositionByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.PositionRouting.List)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetPositionsResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetPositionsResponse>>>> GetPositionList()
        {
            var response = await mediator.Send(new GetPositionsQuery());
            return NewResult(response);
        }


        [HttpPost(Router.PositionRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> CreatePosition([FromBody] AddPositionCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }


        [HttpPut(Router.PositionRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdatePosition([FromBody] EditPositionCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.PositionRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeletePosition([FromRoute] int id)
        {
            var response = await mediator.Send(new DeletePositionCommand(id));
            return NewResult(response);
        }
    }
}

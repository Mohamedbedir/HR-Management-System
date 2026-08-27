using HR.Core.Bases;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.Departments.Queries.Models;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.ById)]
        [ProducesResponseType(typeof(Response<GetDepartmentByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetDepartmentByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetDepartmentByIdResponse>>> GetDepartmentById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetDepartmentByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.DepartmentRouting.List)]
        [ProducesResponseType(typeof(Response<GetDepartmentsResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetDepartmentsResponse>>>> GetDepartmentList()
        {
            var response = await mediator.Send(new GetDepartmentsQuery());
            return NewResult(response);
        }

        [HttpPost(Router.DepartmentRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> CreateDepartment([FromBody] AddDepartmentCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }


        [HttpPut(Router.DepartmentRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteDepartment([FromBody] EditDepartmentCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.DepartmentRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteDepartment([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteDepartmentCommand(id));
            return NewResult(response);
        }
    }
}

using HR.Core.Bases;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.Departments.Queries.Models;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.Employees.Commands.Models;
using HR.Core.Features.Employees.Queries.Models;
using HR.Core.Features.Employees.Queries.Responses;
using HR.Data.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    [ApiController]
    public class EmployeeController : AppControllerBase
    {
        [HttpGet(Router.EmployeeRouting.ById)]
        [ProducesResponseType(typeof(Response<GetEmployeeByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetEmployeeByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetEmployeeByIdResponse>>> GetEmployeeById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetEmployeeByIdQuery(id));
            return NewResult(response);
        }

        [HttpGet(Router.EmployeeRouting.List)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetEmployeesResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<GetEmployeesResponse>>>> GetEmployeesList()
        {
            var response = await mediator.Send(new GetEmployeesQuery());
            return NewResult(response);
        }

        [HttpPost(Router.EmployeeRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> CreateEmployee([FromForm] AddEmployeeCommand model)
        {
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        [HttpPut(Router.EmployeeRouting.Update)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdateEmployee(int id,
         [FromForm] EditEmployeeCommand model)
        {
            model.Id = id;

            var response = await mediator.Send(model);

            return NewResult(response);
        }

        [HttpDelete(Router.EmployeeRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteEmployee([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteEmployeeCommand(id));
            return NewResult(response);
        }
    }
}

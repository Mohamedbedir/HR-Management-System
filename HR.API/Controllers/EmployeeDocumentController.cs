using HR.Core.Bases;
using HR.Core.Features.EmployeeDocuments.Commands.Models;
using HR.Core.Features.EmployeeDocuments.Queries.Models;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
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
    public class EmployeeDocumentController : AppControllerBase
    {
        [HttpGet(Router.EmployeeDocumentRouting.ById)]
        [ProducesResponseType(typeof(Response<GetEmployeeDocumentByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<GetEmployeeDocumentByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<GetEmployeeDocumentByIdResponse>>> GetDocumentById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetEmployeeDocumentByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.EmployeeDocumentRouting.ForEmployee)]
        [ProducesResponseType(typeof(Response<List<GetDocumentsForEmployeeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound<List<GetDocumentsForEmployeeResponse>>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<List<GetDocumentsForEmployeeResponse>>>> GetDocumentsForEmployee([FromRoute] int employeeId)
        {
            var response = await mediator.Send(new GetDocumentsForEmployeeQuery(employeeId));
            return NewResult(response);
        }

        [HttpPost(Router.EmployeeDocumentRouting.Create)]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> Create([FromForm] AddEmployeeDocumentCommand model)
        {
            var response = await mediator.Send(model);

            return NewResult(response);
        }

        [HttpPut(Router.EmployeeDocumentRouting.Update)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> UpdateEmployeeDocument([FromRoute] int id,
            [FromForm] UpdateEmployeeDocumentCommand model)
        {
            model.Id = id;
            var response = await mediator.Send(model);
            return NewResult(response);
        }
        [HttpDelete(Router.EmployeeDocumentRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<string>>> DeleteEmployeeDocument([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteEmployeeDocumentCommand(id));
            return NewResult(response);
        }
    }
}

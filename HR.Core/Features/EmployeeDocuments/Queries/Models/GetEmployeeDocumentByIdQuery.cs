using HR.Core.Bases;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.EmployeeDocuments.Queries.Models
{
    public class GetEmployeeDocumentByIdQuery:IRequest<Response<GetEmployeeDocumentByIdResponse>>
    {
        public long Id { get; set; }
        public GetEmployeeDocumentByIdQuery(long id)
        {
            Id = id;
        }
    }
}

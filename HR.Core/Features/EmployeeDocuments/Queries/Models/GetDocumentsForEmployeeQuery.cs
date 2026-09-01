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
    public class GetDocumentsForEmployeeQuery:IRequest<Response<List<GetDocumentsForEmployeeResponse>>>
    {
        public int EmployeeId { get; set; }
        public GetDocumentsForEmployeeQuery(int id)
        {
            EmployeeId = id;
        }
    }
}

using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.EmployeeDocuments.Commands.Models
{
    public class DeleteEmployeeDocumentCommand
    : IRequest<Response<string>>
    {
        public long Id { get; set; }
        public DeleteEmployeeDocumentCommand(long id)
        {
            Id = id;
        }
    }
}

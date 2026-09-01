using HR.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.EmployeeDocuments.Commands.Models
{
    public class AddEmployeeDocumentCommand: IRequest<Response<string>>
    {
        public int EmployeeId { get; set; }

        public string DocumentType { get; set; } = null!;

        public IFormFile File { get; set; } = null!;

        public DateTime? ExpiryDate { get; set; }
    }
}

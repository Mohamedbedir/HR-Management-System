using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.EmployeeDocuments.Queries.Responses
{
    public class GetEmployeeDocumentByIdResponse
    {
        public long Id { get; set; }

        public string DocumentType { get; set; }

        public string FileName { get; set; }

        public string File { get; set; } = null!;

        public DateTime UploadedAt { get; set; }

        public DateTime? ExpiryDate { get; set; }
             
        public string EmployeeName { get; set; } 
    }
}

using AutoMapper;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.EmployeeDocuments
{
    public partial class EmployeeDocumentProfile:Profile
    {
        public EmployeeDocumentProfile()
        {
            GetEmployeeDocumentByIdMapping();
            GetDocumentsForEmployeeMapping();
        }
    }
}

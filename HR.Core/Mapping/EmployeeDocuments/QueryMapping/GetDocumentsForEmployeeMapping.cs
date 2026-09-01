using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Core.ResolverFile;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.EmployeeDocuments
{
    public partial class EmployeeDocumentProfile
    {
        public void GetDocumentsForEmployeeMapping()
        {
            CreateMap<EmployeeDocument, GetDocumentsForEmployeeResponse>()
               .ForMember(des => des.File, opt => opt.MapFrom<EmpDocumentFileResolver>())
               .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName));
        }
    }
}

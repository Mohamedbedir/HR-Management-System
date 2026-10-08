using HR.Core.Features.Employees.Queries.Responses;
using HR.Core.Pagenation;
using HR.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Employees.Queries.Models
{
    public class GetEmployeesPaginationQuery:IRequest<PaginatedResult<GetEmployeesResponse>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public EmployeeOrderEnum OrderBy { get; set; }
        public EmployeeStatus? FilterByStatus { get; set; }
        public string? Search { get; set; }
    }
}

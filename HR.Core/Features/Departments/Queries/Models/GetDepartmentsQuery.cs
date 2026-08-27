using HR.Core.Bases;
using HR.Core.Features.Departments.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentsQuery : IRequest<Response<IReadOnlyList<GetDepartmentsResponse>>>
    {
    }
}

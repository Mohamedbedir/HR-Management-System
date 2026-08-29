using HR.Core.Bases;
using HR.Core.Features.LeaveTypes.Queries.Responses;
using HR.Core.Features.Positions.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveTypes.Queries.Models
{
    public class GetLeaveTypesQuery:IRequest<Response<IReadOnlyList<GetLeaveTypesResponse>>>
    {
    }
}

using HR.Core.Bases;
using HR.Core.Features.LeaveRequests.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Queries.Models
{
    public class GetLeaveRequestsQuery:IRequest<Response<List<GetLeaveRequestsRespose>>>
    {
    }
}

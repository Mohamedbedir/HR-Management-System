using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Commands.Models
{
    public class CancelLeaveRequestCommand : IRequest<Response<string>>
    {
        public int LeaveRequestId { get; set; }
    }
}

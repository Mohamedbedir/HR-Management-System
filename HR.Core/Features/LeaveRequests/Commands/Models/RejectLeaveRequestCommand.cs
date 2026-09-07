using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Commands.Models
{
    public class RejectLeaveRequestCommand : IRequest<Response<string>>
    {
        public RejectLeaveRequestCommand(int leaveRequestId, string? rejectionReason)
        {
            LeaveRequestId = leaveRequestId;
            RejectionReason = rejectionReason;
        }

        public int LeaveRequestId { get; set; }
        public string? RejectionReason { get; set; }
    }
}

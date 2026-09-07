using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Commands.Models
{
    public class CreateLeaveRequestCommand : IRequest<Response<string>>
    {
        public int EmployeeId { get; set; }

        public int LeaveTypeId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Reason { get; set; }
    }
}

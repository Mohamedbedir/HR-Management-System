using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveTypes.Queries.Responses
{
    public class GetLeaveTypeByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int MaxDaysPerYear { get; set; }

        public bool IsPaid { get; set; }

        public bool IsActive { get; set; }
    }
}

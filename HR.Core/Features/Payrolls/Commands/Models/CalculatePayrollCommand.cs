using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Payrolls.Commands.Models
{
    public class CalculatePayrollCommand : IRequest<Response<string>>
    {
        public int EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal Bonus { get; set; }

        public decimal Deduction { get; set; }
    }
}

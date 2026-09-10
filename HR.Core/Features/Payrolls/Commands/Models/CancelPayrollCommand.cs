using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Payrolls.Commands.Models
{
    public class CancelPayrollCommand:IRequest<Response<string>>
    {
        public int PayrollId { get; set; }
    }
}

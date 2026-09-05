using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Attendances.Commands.Models
{
    public class CheckOutCommand:IRequest<Response<string>>
    {
        public int EmployeeId { get; set; }

        public CheckOutCommand(int id)
        {
            EmployeeId=id;
        }

    }
}

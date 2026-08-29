using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveTypes.Commands.Models
{
    public class DeleteLeaveTypeCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteLeaveTypeCommand(int id)
        {
            Id = id;
        }
    }
}

using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Attendances.Commands.Models
{
    public class CheckInCommand : IRequest<Response<string>>
    {
        public int EmployeeId { get; set; }
        public string Notes { get; set; }
        //public CheckInCommand(int id,string notes)
        //{
        //    EmployeeId = id;
        //    Notes = notes;
        //}

    }
}

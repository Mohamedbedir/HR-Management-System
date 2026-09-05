using HR.Core.Bases;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Data.Entities;
using HR.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Attendances.Queries.Models
{
    public class GetAttendancesForEmpliyeeQuery:IRequest<Response<List<GetAttendancesForEmpliyeeResponse>>>
    {
        public int EmployeeId { get; set; }

        public GetAttendancesForEmpliyeeQuery(int id)
        {
            EmployeeId= id;
        }
    }
}

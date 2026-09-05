using HR.Core.Bases;
using HR.Core.Features.Attendances.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Attendances.Queries.Models
{
    public class GetAttendancesForEmpliyeeByDateQuery:IRequest<Response<List<GetAttendancesForEmpliyeeByDateResponse>>>
    {
        public int EmployeeId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }

        public GetAttendancesForEmpliyeeByDateQuery(int id ,DateOnly fromdate,DateOnly todate)
        {
            EmployeeId = id;
            FromDate = fromdate;
            ToDate = todate;
        }
    }
}

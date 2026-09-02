using HR.Core.Bases;
using HR.Core.Features.Histories.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Histories.Queries.Models
{
    public class GetSalaryHistoryForEmployeeQuery:IRequest<Response<List<GetSalaryHistoryForEmployeeResponse>>>
    {
        public int EmployeeId { get; set; }
        public GetSalaryHistoryForEmployeeQuery(int id)
        {
            EmployeeId=id;
        }
    }
}

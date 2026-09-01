using HR.Core.Bases;
using HR.Core.Features.Employees.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Employees.Queries.Models
{
    public class GetEmployeeByIdQuery:IRequest<Response<GetEmployeeByIdResponse>>
    {
        public int Id { get; set; }
        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}

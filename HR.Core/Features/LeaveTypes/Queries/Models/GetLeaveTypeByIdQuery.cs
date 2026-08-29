using HR.Core.Bases;
using HR.Core.Features.LeaveTypes.Queries.Responses;
using HR.Core.Features.Positions.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveTypes.Queries.Models
{
    public class GetLeaveTypeByIdQuery:IRequest<Response<GetLeaveTypeByIdResponse>>
    {
        public int Id { get; set; }
        public GetLeaveTypeByIdQuery(int id)
        {
            Id=id;
        }
    }
}

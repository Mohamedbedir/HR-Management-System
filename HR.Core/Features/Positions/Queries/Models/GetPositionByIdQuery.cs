using HR.Core.Bases;
using HR.Core.Features.Positions.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Positions.Queries.Models
{
    public class GetPositionByIdQuery:IRequest<Response<GetPositionByIdResponse>>
    {
        public int Id { get; set; }
        public GetPositionByIdQuery(int id)
        {
            Id=id;
        }
    }
}

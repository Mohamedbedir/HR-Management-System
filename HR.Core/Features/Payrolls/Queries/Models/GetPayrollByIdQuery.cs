using HR.Core.Bases;
using HR.Core.Features.Payrolls.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Payrolls.Queries.Models
{
    public class GetPayrollByIdQuery: IRequest<Response<GetPayrollByIdResponse>>
    {
        public int Id { get; set; }

        public GetPayrollByIdQuery(int id)
        {
            Id = id;
        }
    }
}

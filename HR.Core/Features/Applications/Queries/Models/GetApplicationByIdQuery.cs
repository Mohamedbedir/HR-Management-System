using HR.Core.Bases;
using HR.Core.Features.Applications.Queries.Responses;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.JobPostings.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Applications.Queries.Models
{
    public class GetApplicationByIdQuery : IRequest<Response<GetApplicationByIdResponse>>
    {
        public int Id { get; set; }
        public GetApplicationByIdQuery(int id) {
        
            Id = id;
        }
    }
}

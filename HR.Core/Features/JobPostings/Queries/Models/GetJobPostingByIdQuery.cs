using HR.Core.Bases;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.JobPostings.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.JobPostings.Queries.Models
{
    public class GetJobPostingByIdQuery : IRequest<Response<GetJobPostingByIdResponse>>
    {
        public int Id { get; set; }
        public GetJobPostingByIdQuery(int id) {
        
            Id = id;
        }
    }
}

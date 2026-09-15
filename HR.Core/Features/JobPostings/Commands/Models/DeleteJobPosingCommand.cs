using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.JobPostings.Commands.Models
{
    public class DeleteJobPostingCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteJobPostingCommand(int id)
        {
            Id = id;
        }
    }
}

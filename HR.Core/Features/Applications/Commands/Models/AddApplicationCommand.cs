using HR.Core.Bases;
using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
using HR.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Applications.Commands.Models
{
    public class AddApplicationCommand : IRequest<Response<string>>
    {
        public string? Notes { get; set; }

        // Navigation
        public int CandidateId { get; set; }
        public int JobPostingId { get; set; }
    }
}

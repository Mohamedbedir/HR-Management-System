using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Applications.Queries.Responses
{
    public class GetApplicationByIdResponse
    {
        public int Id { get; set; }
        public DateTime AppliedAt { get; set; }

        public ApplicationStatus Status { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public int CandidateId { get; set; }
        public string CandidateName { get; set; } = null!;
        public int JobPostingId { get; set; }
        public string JobPostTitle { get; set; } = null!;


    }
}

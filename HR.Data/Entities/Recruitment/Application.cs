using HR.Data.Entities.Common;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities.Recruitment
{
    public class Application:BaseEntity
    {
        public DateTime AppliedAt { get; set; }

        public ApplicationStatus Status { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; } = null!;
        public int JobPostingId { get; set; }
        public JobPosting JobPosting { get; set; } = null!;
    }
}

using HR.Data.Entities.Common;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HR.Data.Entities.Recruitment
{
    public class JobPosting : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public JobPostingStatus Status { get; set; }

        public DateTime PostedAt { get; set; }

        public DateTime? ClosingDate { get; set; }

        // Navigation
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;

        public ICollection<Application> Applications { get; set; }
            = new HashSet<Application>();
    }
}

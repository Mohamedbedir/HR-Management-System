using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.JobPostings.Commands.Models
{
    public class EditJobPostingCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public DateTime? ClosingDate { get; set; }

        public int DepartmentId { get; set; }

        public int PositionId { get; set; }
    }
}

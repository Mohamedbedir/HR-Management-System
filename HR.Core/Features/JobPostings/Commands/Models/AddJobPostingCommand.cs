using HR.Core.Bases;
using HR.Data.Entities;
using HR.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.JobPostings.Commands.Models
{
    public class AddJobPostingCommand : IRequest<Response<string>>
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public DateTime? ClosingDate { get; set; }

        public int DepartmentId { get; set; }

        public int PositionId { get; set; }

    }
}

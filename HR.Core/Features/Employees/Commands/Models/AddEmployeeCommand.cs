using HR.Core.Bases;
using HR.Data.Entities;
using HR.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Employees.Commands.Models
{
    public class AddEmployeeCommand : IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }
        public decimal Salary { get; set; }
        public EmployeeStatus? Status { get; set; }
        public Gender? Gender { get; set; }

        // Relations
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public int? ManagerId { get; set; }
    }
}

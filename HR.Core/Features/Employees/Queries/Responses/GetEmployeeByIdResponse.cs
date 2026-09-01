using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Employees.Queries.Responses
{
    public class GetEmployeeByIdResponse
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; private set; } = null!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }
        public DateOnly HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }
        public decimal Salary { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

     
        public string? DepartmentName { get; set; }
        public string? PositionTitle { get; set; }
        public string? ManagerName { get; set; }

        public List<SubordinateEmployee> Subordinates { get; set; }

    }

    public class SubordinateEmployee
    {
        public int Id {get; set; }
        public string EmployeeNumber { get; private set; } = null!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }
        public DateOnly HireDate { get; set; }
    }
}

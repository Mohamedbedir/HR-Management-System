using HR.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class Employee : BaseEntity, IAuditable
    {
        //public int Id { get; set; }
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

        // Relations
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? PositionId { get; set; }
        public Position? Position { get; set; }
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        public ICollection<Employee> Subordinates { get; set; } = new HashSet<Employee>();



    }
}

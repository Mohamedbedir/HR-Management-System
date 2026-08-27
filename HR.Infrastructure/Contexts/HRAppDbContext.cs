using HR.Data.Entities;
using HR.Data.Entities.Common;
using HR.Data.Entities.Recruitment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Contexts
{
    public class HRAppDbContext :DbContext
    {
        public HRAppDbContext(DbContextOptions<HRAppDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(
          CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<IAuditable>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;

                        break;

                    case EntityState.Modified:

                        entry.Entity.UpdatedAt = DateTime.UtcNow;

                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PayrollItem> PayrollItems { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }

    }
}

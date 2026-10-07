using HR.Data.Enums;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.DTOs;
using HR.Service.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IEmployeeRepo employeeRepo;
        private readonly IAttendanceRepo attendanceRepo;
        private readonly ILeaveRequestRepo leaveRequestRepo;
        private readonly IPayrollRepo payrollRepo;
        private readonly IPerformanceReviewRepo performanceRepo;

        public DashboardService(IEmployeeRepo employeeRepo,
            IAttendanceRepo attendanceRepo,
            ILeaveRequestRepo leaveRequestRepo,
            IPayrollRepo payrollRepo ,
            IPerformanceReviewRepo performanceRepo)
        {
            this.employeeRepo = employeeRepo;
            this.attendanceRepo = attendanceRepo;
            this.leaveRequestRepo = leaveRequestRepo;
            this.payrollRepo = payrollRepo;
            this.performanceRepo = performanceRepo;
        }
        public async Task<DashboardStatisticsDto> GetStatisticsAsync(
         CancellationToken cancellationToken = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var currentMonth = DateTime.Today.Month;
            var currentYear = DateTime.Today.Year;

            var totalEmployees = await employeeRepo
                    .GetTableNoTracking()
                    .CountAsync(e => e.Status == EmployeeStatus.Active,
                    cancellationToken);

            var presentToday = await attendanceRepo
                    .GetTableNoTracking()
                    .Where(a => a.Date == today)
                    .CountAsync(a =>
                        a.Date == today &&
                        a.CheckIn != null,
                    cancellationToken);

            var pendingLeaves = await leaveRequestRepo
                    .GetTableNoTracking()
                    .CountAsync(
                        l => l.Status == LeaveRequestStatus.Pending,
                        cancellationToken);

            var monthlyPayroll = await payrollRepo
                    .GetTableNoTracking()
                    .Where(p =>
                        p.Month == currentMonth &&
                        p.Year == currentYear)
                    .SumAsync(
                        p => p.NetSalary,
                        cancellationToken);

            return new DashboardStatisticsDto
            {
                TotalEmployees = totalEmployees,
                PresentToday = presentToday,
                PendingLeaves = pendingLeaves,
                MonthlyPayroll = monthlyPayroll
            };
        }

        public async Task<IReadOnlyList<RecentActivityDto>> GetRecentActivitiesAsync( int count = 10,
            CancellationToken cancellationToken = default)
        {
            var activities = new List<RecentActivityDto>();

            var employees = await employeeRepo
                .GetTableNoTracking()
                .Include(d=>d.Department)
                .OrderByDescending(e => e.CreatedAt)
                .Take(count)
                .Select(e => new RecentActivityDto
                {
                    Title = "New employee added",
                    Description = $"{e.FirstName} {e.LastName} joined the {e.Department.Name} Department.",
                    Time = e.CreatedAt,
                    Icon = "bi-person-plus-fill"
                })
                .ToListAsync(cancellationToken);

            activities.AddRange(employees);

            var leaveRequests = await leaveRequestRepo
                .GetTableNoTracking()
                .OrderByDescending(l => l.CreatedAt)
                .Take(count)
                .Select(l => new RecentActivityDto
                {
                    Title = "Leave request submitted",
                    Description = $"Leave request submitted by {l.Employee.FirstName} {l.Employee.LastName}.",
                    Time = l.CreatedAt,
                    Icon = "bi-calendar-plus"
                })
                .ToListAsync(cancellationToken);

            activities.AddRange(leaveRequests);

            var payrolls = await payrollRepo
                .GetTableNoTracking()
                .OrderByDescending(p => p.GeneratedAt)
                .Take(count)
                .Select(p => new RecentActivityDto
                {
                    Title = "Payroll generated",
                    Description = $"Payroll generated for {p.Employee.FirstName} {p.Employee.LastName}.",
                    Time = p.GeneratedAt,
                    Icon = "bi-calculator-fill"
                })
                .ToListAsync(cancellationToken);

            activities.AddRange(payrolls);

            var reviews = await performanceRepo
                .GetTableNoTracking()
                .OrderByDescending(r => r.CreatedAt)
                .Take(count)
                .Select(r => new RecentActivityDto
                {
                    Title = "Performance review completed",
                    Description = $"Performance review completed for {r.Employee.FirstName} {r.Employee.LastName}.",
                    Time = r.CreatedAt,
                    Icon = "bi-clipboard-check-fill"
                })
                .ToListAsync(cancellationToken);

            activities.AddRange(reviews);

            return activities
                .OrderByDescending(a => a.Time)
                .Take(count)
                .ToList();
        }
    }
}

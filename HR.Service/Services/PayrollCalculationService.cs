using HR.Data.Entities;
using HR.Data.Enums;
using HR.Service.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class PayrollCalculationService : IPayrollCalculationService
    {
        private readonly IEmployeeService employeeService;
        private readonly IAttendanceService attendanceService;
        private readonly ILeaveRequestService leaveRequestService;

        public PayrollCalculationService(
            IEmployeeService employeeService,
            IAttendanceService attendanceService,
            ILeaveRequestService leaveRequestService)
        {
            this.employeeService = employeeService;
            this.attendanceService = attendanceService;
            this.leaveRequestService = leaveRequestService;
        }

        public async Task<Payroll> CalculateAsync(
            Employee employee,
            int month,
            int year,
            decimal bonus,
            decimal deduction)
        {
            // =========================
            // 1. Get Employee
            // =========================

            //var employee = await employeeService.GetEmployeeByIdAsync(employeeId);

            // =========================
            // 2. Get Basic Salary
            // =========================

            var basicSalary = employee.Salary;


            // =========================
            // 3. Salary Rules
            // =========================

            const decimal salaryDays = 30;
            const decimal workingHoursPerDay = 8;
            const decimal overtimeMultiplier = 1.5m;

            var dailySalary = basicSalary / salaryDays;

            var hourlySalary =
                dailySalary / workingHoursPerDay;


            // =========================
            // 4. Get Attendance
            // =========================

            var fromDate = new DateOnly(year, month, 1);

            var toDate = fromDate.AddMonths(1).AddDays(-1);

            var attendances =
                await attendanceService.GetEmployeeAttendancesByDateAsync(
                    employee.Id,
                    fromDate,
                    toDate);


            // =========================
            // 5. Calculate Overtime
            // =========================

            var totalOvertimeMinutes =
                attendances.Sum(x => x.OvertimeMinutes);

            var overtimeAmount =
                hourlySalary / 60
                * totalOvertimeMinutes
                * overtimeMultiplier;


            // =========================
            // 6. Get Approved Unpaid Leaves
            // =========================

            var leaveRequests =
                await leaveRequestService
                    .GetEmployeeLeaveRequestsAsync(employee.Id);

            var unpaidLeaveDays = leaveRequests
                .Where(x =>
                    x.Status == LeaveRequestStatus.Approved &&
                    x.LeaveType != null &&
                    x.LeaveType.Name == "Unpaid" &&
                    x.StartDate <= toDate &&
                    x.EndDate >= fromDate)
                .Sum(x =>
                {
                    var start = x.StartDate < fromDate
                        ? fromDate
                        : x.StartDate;

                    var end = x.EndDate > toDate
                        ? toDate
                        : x.EndDate;

                    return end.DayNumber - start.DayNumber + 1;
                });


            // =========================
            // 7. Unpaid Leave Deduction
            // =========================

            var unpaidLeaveDeduction =
                dailySalary * unpaidLeaveDays;


            // =========================
            // 8. Gross Salary
            // =========================

            var totalEarnings =
                overtimeAmount + bonus;

            var grossSalary =
                basicSalary + totalEarnings;


            // =========================
            // 9. Total Deductions
            // =========================

            var totalDeductions =
                unpaidLeaveDeduction + deduction;


            // =========================
            // 10. Net Salary
            // =========================

            var netSalary =
                grossSalary - totalDeductions;


            // =========================
            // 11. Create Payroll
            // =========================

            var payroll = new Payroll
            {
                EmployeeId = employee.Id,

                Month = month,
                Year = year,

                BasicSalary = basicSalary,

                GrossSalary = grossSalary,

                TotalDeductions = totalDeductions,

                NetSalary = netSalary,

                Status = PayrollStatus.Calculated,

                GeneratedAt = DateTime.Now
            };


            // =========================
            // 12. Add Overtime Item
            // =========================

            if (overtimeAmount > 0)
            {
                payroll.Items.Add(new PayrollItem
                {
                    Name = "Overtime",
                    Type = PayrollItemType.Overtime,
                    Amount = overtimeAmount,
                    Description =
                        $"Overtime: {totalOvertimeMinutes} minutes"
                });
            }


            // =========================
            // 13. Add Bonus Item
            // =========================

            if (bonus > 0)
            {
                payroll.Items.Add(new PayrollItem
                {
                    Name = "Bonus",
                    Type = PayrollItemType.Bonus,
                    Amount = bonus,
                    Description = "Additional bonus"
                });
            }


            // =========================
            // 14. Add Unpaid Leave Item
            // =========================

            if (unpaidLeaveDeduction > 0)
            {
                payroll.Items.Add(new PayrollItem
                {
                    Name = "Unpaid Leave",
                    Type = PayrollItemType.UnpaidLeave,
                    Amount = unpaidLeaveDeduction,
                    Description =
                        $"Unpaid leave: {unpaidLeaveDays} days"
                });
            }


            // =========================
            // 15. Add Other Deduction
            // =========================

            if (deduction > 0)
            {
                payroll.Items.Add(new PayrollItem
                {
                    Name = "Deduction",
                    Type = PayrollItemType.Deduction,
                    Amount = deduction,
                    Description = "Additional deduction"
                });
            }

            return payroll;
        }
    }
}

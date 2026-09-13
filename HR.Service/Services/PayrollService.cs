using HR.Data.Entities;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepo payrollRepo;

        public PayrollService(IPayrollRepo payrollRepo)
        {
            this.payrollRepo = payrollRepo;
        }

        public async Task<Payroll?> GetByIdAsync(int id)
        {
            return await payrollRepo
                .GetTableNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Payroll>> GetAllAsync()
        {
            return await payrollRepo
                .GetTableNoTracking()
                .Include(x => x.Items)
                .Include(x => x.Employee)
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();
        }

        public async Task<List<Payroll>> GetEmployeePayrollsAsync(int employeeId)
        {
            return await payrollRepo
                .GetTableNoTracking()
                .Include(x => x.Items)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();
        }

        public async Task<bool> IsPayrollExistAsync(int id)
        {
            return await payrollRepo
                .GetTableNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsPayrollExistForMonthAsync(
            int employeeId,
            int month,
            int year)
        {
            return await payrollRepo
                .GetTableNoTracking()
                .AnyAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.Month == month &&
                    x.Year == year);
        }

        public async Task AddAsync(Payroll payroll)
        {
            await payrollRepo.AddAsync(payroll);
        }

        public async Task UpdateAsync(Payroll payroll)
        {
            payrollRepo.UpdateAsync(payroll);
        }

        public async Task SaveChangesAsync()
        {
            await payrollRepo.SaveChangesAsync();
        }
    }
}

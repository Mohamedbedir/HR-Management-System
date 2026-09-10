using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IPayrollService
    {
        Task<Payroll?> GetByIdAsync(int id);

        Task<List<Payroll>> GetAllAsync();

        Task<List<Payroll>> GetEmployeePayrollsAsync(int employeeId);

        Task<bool> IsPayrollExistAsync(int id);

        Task<bool> IsPayrollExistForMonthAsync(
            int employeeId,
            int month,
            int year);

        Task AddAsync(Payroll payroll);

        Task UpdateAsync(Payroll payroll);

        Task SaveChangesAsync();
    }
}

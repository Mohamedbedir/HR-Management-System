using HR.Data.Entities;
using HR.Infrastructure.Repositories;
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
    public class SalaryHistoryService : ISalaryHistoryService
    {
        private readonly ISalaryHistoryRepo salaryHistoryRepo;

        public SalaryHistoryService(ISalaryHistoryRepo salaryHistoryRepo)
        {
            this.salaryHistoryRepo = salaryHistoryRepo;
        }
        public async Task<List<SalaryHistory>> GetSalaryHistoriesAsync(int employeeId)
        {
            return await salaryHistoryRepo
                .GetTableAsTracking()
                .Where(x => x.EmployeeId == employeeId)
                .Include(e => e.Employee)
                .ToListAsync();
        }

        public Task<SalaryHistory?> GetSalaryHistoryByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}

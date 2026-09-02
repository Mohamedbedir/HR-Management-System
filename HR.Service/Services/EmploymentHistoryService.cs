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
    public class EmploymentHistoryService : IEmploymentHistoryService
    {
        private readonly IEmploymentHistoryRepo employmentHistoryRepo;

        public EmploymentHistoryService(IEmploymentHistoryRepo employmentHistoryRepo)
        {
            this.employmentHistoryRepo = employmentHistoryRepo;
        }
        public async Task<List<EmploymentHistory>> GetEmploymentHistoriesAsync(int employeeId)
        {
            return await employmentHistoryRepo
                .GetTableAsTracking()
                .Where(x => x.EmployeeId == employeeId)
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .Include(e => e.Department)
                .ToListAsync();
        }

        public Task<EmploymentHistory?> GetEmploymentHistoryByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}

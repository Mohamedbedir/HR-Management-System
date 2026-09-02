using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface ISalaryHistoryService
    {
        Task<SalaryHistory?> GetSalaryHistoryByIdAsync(long id);

        Task<List<SalaryHistory>> GetSalaryHistoriesAsync(int employeeId);
    }
}

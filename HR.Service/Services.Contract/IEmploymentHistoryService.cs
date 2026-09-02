using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IEmploymentHistoryService
    {
        Task<EmploymentHistory?> GetEmploymentHistoryByIdAsync(long id);

        Task<List<EmploymentHistory>> GetEmploymentHistoriesAsync(int employeeId);
    }
}

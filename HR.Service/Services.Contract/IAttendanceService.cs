using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IAttendanceService
    {
        Task<Attendance?> GetByIdAsync(int id);

        Task<Attendance?> GetEmployeeAttendanceByDateAsync(int employeeId, DateOnly date);
        Task<List<Attendance>> GetEmployeeAttendancesAsync(int employeeId);

        Task<List<Attendance>> GetEmployeeAttendancesByDateAsync(int employeeId, DateOnly? fromDate = null,
            DateOnly? toDate = null);

        Task<string> AddAsync(Attendance attendance);

        Task<string> UpdateAsync(Attendance attendance);
    }
}

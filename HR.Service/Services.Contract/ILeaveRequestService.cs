using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface ILeaveRequestService
    {
        Task<LeaveRequest?> GetByIdAsync(int id);
        Task<LeaveRequest?> GetByIdIncludeAsync(int id);

        Task<List<LeaveRequest>> GetAllAsync();

        Task<List<LeaveRequest>> GetEmployeeLeaveRequestsAsync(int employeeId);

        Task<bool> IsLeaveRequestExistAsync(int id);

        Task<bool> IsOverlappingLeaveRequestAsync(int employeeId,
            DateOnly startDate,
            DateOnly endDate);

        Task<string> AddAsync(LeaveRequest leaveRequest);

        Task<string> UpdateAsync(LeaveRequest leaveRequest);

        Task SaveChangesAsync();
    }
}

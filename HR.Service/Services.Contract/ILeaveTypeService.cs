using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface ILeaveTypeService
    {
        Task<LeaveType?> GetLeaveTypeByIdAsync(int id);

        Task<IReadOnlyList<LeaveType>> GetAllLeaveTypesAsync();

        Task<string> CreateLeaveTypeAsync(LeaveType LeaveType);

        Task<string> UpdateLeaveTypeAsync(LeaveType LeaveType);

        Task<string> DeleteLeaveTypeAsync(LeaveType LeaveType);
        Task<bool> IsLeaveTypeExist(string name);
        Task<bool> IsLeaveTypeExistExcludeSelf(string name, int id);
    }
}

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
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepo leaveTypeRepo;

        public LeaveTypeService(ILeaveTypeRepo leaveTypeRepo)
        {
            this.leaveTypeRepo = leaveTypeRepo;
        }
        public async Task<string> CreateLeaveTypeAsync(LeaveType LeaveType)
        {
            await leaveTypeRepo.AddAsync(LeaveType);
            await leaveTypeRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> DeleteLeaveTypeAsync(LeaveType LeaveType)
        {
            leaveTypeRepo.DeleteAsync(LeaveType);
            await leaveTypeRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<IReadOnlyList<LeaveType>> GetAllLeaveTypesAsync()
        {
            return await leaveTypeRepo.GetAllAsync();
        }

        public async Task<LeaveType?> GetLeaveTypeByIdAsync(int id)
        {
           return await leaveTypeRepo.GetByIdAsync(id);
        }

        public async Task<bool> IsLeaveTypeExist(string name)
        {
            return await leaveTypeRepo.GetTableNoTracking().AnyAsync(l=>l.Name ==name);
        }

        public async Task<bool> IsLeaveTypeExistExcludeSelf(string name, int id)
        {
            var type = await leaveTypeRepo.GetTableNoTracking()
                .Where(d => d.Name == name && !d.Id.Equals(id)).FirstOrDefaultAsync();
            if (type == null)
                return false;
            return true;
        }

        public async Task<string> UpdateLeaveTypeAsync(LeaveType LeaveType)
        {
            leaveTypeRepo.UpdateAsync(LeaveType);
            await leaveTypeRepo.SaveChangesAsync();
            return "Success";
        }
    }
}

using HR.Data.Entities;
using HR.Data.Enums;
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
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepo leaveRequestRepo;

        public LeaveRequestService(ILeaveRequestRepo leaveRequestRepo)
        {
            this.leaveRequestRepo = leaveRequestRepo;
        }

        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            return await leaveRequestRepo
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<LeaveRequest>> GetAllAsync()
        {
            return await leaveRequestRepo
                .GetTableNoTracking()
                .Include(e=>e.Employee)
                .Include(e=>e.LeaveType).Include(e => e.ApprovedBy)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetEmployeeLeaveRequestsAsync(
            int employeeId)
        {
            return await leaveRequestRepo
                .GetTableNoTracking()
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.ApprovedBy)
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        public async Task<bool> IsLeaveRequestExistAsync(int id)
        {
            return await leaveRequestRepo
                .GetTableNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsOverlappingLeaveRequestAsync(
            int employeeId,
            DateOnly startDate,
            DateOnly endDate)
        {
            return await leaveRequestRepo
                .GetTableNoTracking()
                .AnyAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.Status != LeaveRequestStatus.Rejected &&
                    x.StartDate <= endDate &&
                    x.EndDate >= startDate);
        }

        public async Task<string> AddAsync(LeaveRequest leaveRequest)
        {
            await leaveRequestRepo.AddAsync(leaveRequest);

            return "Success";
        }

        public async Task<string> UpdateAsync(LeaveRequest leaveRequest)
        {
            leaveRequestRepo.UpdateAsync(leaveRequest);

            return "Success";
        }

        public async Task SaveChangesAsync()
        {
            await leaveRequestRepo.SaveChangesAsync();
        }

        public async Task<LeaveRequest?> GetByIdIncludeAsync(int id)
        {
            return await leaveRequestRepo
               .GetTableNoTracking()
               .Include(e => e.Employee)
               .Include(e => e.LeaveType).Include(e => e.ApprovedBy)
               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

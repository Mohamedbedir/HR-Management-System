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
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepo attendanceRepo;

        public AttendanceService(
            IAttendanceRepo attendanceRepo)
        {
            this.attendanceRepo = attendanceRepo;
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await attendanceRepo
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Attendance>> GetEmployeeAttendancesByDateAsync( int employeeId,
         DateOnly? fromDate = null,
     DateOnly? toDate = null)
        {
            var query = attendanceRepo
                .GetTableNoTracking()
                .Include(e => e.Employee)
                .Where(x => x.EmployeeId == employeeId);

            if (fromDate.HasValue)
                query = query.Where(x => x.Date >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(x => x.Date <= toDate.Value);

            return await query
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<Attendance>> GetEmployeeAttendancesAsync( int employeeId)
        {
            var query = attendanceRepo
                .GetTableNoTracking()
                .Include(e=>e.Employee)
                .Where(x => x.EmployeeId == employeeId);
            
            return await query.ToListAsync();
        }

        public async Task<Attendance?> GetEmployeeAttendanceByDateAsync(int employeeId,DateOnly date)
        {
            return await attendanceRepo
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.Date == date);
        }

        public async Task<string> AddAsync(Attendance attendance)
        {
            await attendanceRepo.AddAsync(attendance);
            return "Success";
        }

        public async Task<string> UpdateAsync(Attendance attendance)
        {
            attendanceRepo.UpdateAsync(attendance);
            return "Success";
        }

        public async Task SaveChangesAsync()
        {
            await attendanceRepo.SaveChangesAsync();
        }
    }
}

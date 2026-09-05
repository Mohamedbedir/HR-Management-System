using HR.Core.Bases;
using HR.Core.Features.Attendances.Commands.Models;
using HR.Core.Localization;
using HR.Data.Entities;
using HR.Data.Enums;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Attendances.Commands.Handlers
{
    public class AttendenceCommandHandler: ResponseHandler
        , IRequestHandler<CheckInCommand, Response<string>>
        , IRequestHandler<CheckOutCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IEmployeeService employeeService;
        private readonly IAttendanceService attendanceService;

        public AttendenceCommandHandler(IStringLocalizer<SharedResources> localizer,
            IEmployeeService employeeService,
            IAttendanceService attendanceService):base(localizer)
        {
            this.localizer = localizer;
            this.employeeService = employeeService;
            this.attendanceService = attendanceService;
        }

        public async Task<Response<string>> Handle(CheckInCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Get Employee
            var employee = await employeeService.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null)
                return NotFound<string>("Employee not found.");

            // 2. Check Employee Status
            if (employee.Status != EmployeeStatus.Active)
                return BadRequest<string>("Employee is not active.");

            // 3. Get current date and time
            var now = DateTime.Now;

            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

            // 4. Check if attendance already exists today
            var existingAttendance =
                await attendanceService.GetEmployeeAttendanceByDateAsync(
                    request.EmployeeId,
                    today);

            if (existingAttendance != null)
                return Conflict<string>("Employee already checked in today.");

            var workStartTime = new TimeOnly(9, 0);

            var lateMinutes = 0;
            var status = AttendanceStatus.Present;

            if (currentTime > workStartTime)
            {
                lateMinutes = (int)(currentTime - workStartTime).TotalMinutes;
                status = AttendanceStatus.Late;
            }

            // 5. Create Attendance
            var attendance = new Attendance
            {
                EmployeeId = request.EmployeeId,
                Date = today,
                CheckIn = currentTime,
                Status = status,
                LateMinutes = lateMinutes,
                Notes= request.Notes,
                OvertimeMinutes = 0
            };

            // 6. Add Attendance
            await attendanceService.AddAsync(attendance);

            // 7. Save Changes
            await attendanceService.SaveChangesAsync();

            return Success<string>(entity:null,Message:"Check-in completed successfully.");
        }

        public async Task<Response<string>> Handle( CheckOutCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Check Employee
            var employee = await employeeService.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null)
                return NotFound<string>("Employee not found.");

            // 2. Check Employee Status
            if (employee.Status != EmployeeStatus.Active)
                return BadRequest<string>("Employee is not active.");

            // 3. Get today's attendance
            var today = DateOnly.FromDateTime(DateTime.Now);

            var attendance =
                await attendanceService.GetEmployeeAttendanceByDateAsync(
                    request.EmployeeId,
                    today);

            if (attendance == null)
                return NotFound<string>("No check-in found for today.");

            // 4. Check if already checked out
            if (attendance.CheckOut.HasValue)
                return Conflict<string>("Employee has already checked out today.");

            // 5. Current time
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            // 6. Make sure CheckOut is after CheckIn
            if (attendance.CheckIn.HasValue &&
                currentTime < attendance.CheckIn.Value)
            {
                return BadRequest<string>(
                    "Check-out time cannot be earlier than check-in time.");
            }

            // 7. Official work end time
            var workEndTime = new TimeOnly(17, 0);

            // 8. Calculate overtime
            var overtimeMinutes = 0;

            if (currentTime > workEndTime)
            {
                overtimeMinutes =
                    (int)(currentTime - workEndTime).TotalMinutes;
            }

            // 9. Calculate total worked minutes
            var workedMinutes = 0;

            if (attendance.CheckIn.HasValue)
            {
                workedMinutes =
                    (int)(currentTime - attendance.CheckIn.Value).TotalMinutes;
            }

            // 10. Determine status
            if (workedMinutes < 240)
            {
                attendance.Status = AttendanceStatus.HalfDay;
            }
            else if (attendance.LateMinutes > 0)
            {
                attendance.Status = AttendanceStatus.Late;
            }
            else
            {
                attendance.Status = AttendanceStatus.Present;
            }

            // 11. Update attendance
            attendance.CheckOut = currentTime;
            attendance.OvertimeMinutes = overtimeMinutes;

            await attendanceService.UpdateAsync(attendance);

            // 12. Save changes
            await attendanceService.SaveChangesAsync();

            return Success<string>(
                "Check-out completed successfully.");
        }
    }
}

using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Attendances.Queries.Models;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Data.AppMetaData;
using HR.Service.Services;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Attendances.Queries.Handlers
{
    public class AttendanceQueryHandler : ResponseHandler,
        IRequestHandler<GetAttendancesForEmpliyeeQuery, Response<List<GetAttendancesForEmpliyeeResponse>>>,
        IRequestHandler<GetAttendancesForEmpliyeeByDateQuery, Response<List<GetAttendancesForEmpliyeeByDateResponse>>>
    {
        private readonly IAttendanceService attendanceService;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly ICurrentUserService currentUserService;

        public AttendanceQueryHandler(IAttendanceService attendanceService,
            IEmployeeService employeeService,
            IMapper mapper,IStringLocalizer<SharedResources> localizer,
            ICurrentUserService currentUserService) : base(localizer)
        {
            this.attendanceService = attendanceService;
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.localizer = localizer;
            this.currentUserService = currentUserService;
        }

        public async Task<Response<List<GetAttendancesForEmpliyeeResponse>>> Handle(GetAttendancesForEmpliyeeQuery request, CancellationToken cancellationToken)
        {
            // 1. Check if employee exists
            var isEmployeeExist =
                await employeeService.IsEmployeeExistById(request.EmployeeId);

            if (!isEmployeeExist)
                return NotFound<List<GetAttendancesForEmpliyeeResponse>>();

            // 2. Admin and HR can see any employee's attendance
            if (currentUserService.IsInRole(Roles.Admin) ||
                currentUserService.IsInRole(Roles.HR))
            {
                var attendances =
                    await attendanceService.GetEmployeeAttendancesAsync(
                        request.EmployeeId);

                var mapped =
                    mapper.Map<List<GetAttendancesForEmpliyeeResponse>>(attendances);

                return Success(mapped);
            }

            // 3. Employee can see only his own attendance
            if (currentUserService.IsInRole(Roles.Employee))
            {
                if (currentUserService.EmployeeId != request.EmployeeId)
                    return Forbidden<List<GetAttendancesForEmpliyeeResponse>>("You can only view your own attendance.");

                var attendances =
                    await attendanceService.GetEmployeeAttendancesAsync(
                        request.EmployeeId);

                var mapped =
                    mapper.Map<List<GetAttendancesForEmpliyeeResponse>>(attendances);

                return Success(mapped);
            }

            // 4. Manager can see only his subordinates
            if (currentUserService.IsInRole(Roles.Manager))
            {
                var currentEmployeeId = currentUserService.EmployeeId;

                if (!currentEmployeeId.HasValue)
                    return Unauthorized<List<GetAttendancesForEmpliyeeResponse>>();

                var isSubordinate =
                    await employeeService.IsEmployeeUnderManagerAsync(
                        request.EmployeeId,
                        currentEmployeeId.Value);

                if (!isSubordinate)
                    return Forbidden<List<GetAttendancesForEmpliyeeResponse>>("You can only view your subordinates' attendance.");

                var attendances =
                    await attendanceService.GetEmployeeAttendancesAsync(
                        request.EmployeeId);

                var mapped =
                    mapper.Map<List<GetAttendancesForEmpliyeeResponse>>(attendances);

                return Success(mapped);
            }

            // 5. Any other role
            return Unauthorized<List<GetAttendancesForEmpliyeeResponse>>();
        }

        public async Task<Response<List<GetAttendancesForEmpliyeeByDateResponse>>> Handle(GetAttendancesForEmpliyeeByDateQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetAttendancesForEmpliyeeByDateResponse>>();
            // 2. Admin and HR can see any employee's attendance
            if (currentUserService.IsInRole(Roles.Admin) ||
                currentUserService.IsInRole(Roles.HR))
            {
                var Attendance = await attendanceService
                .GetEmployeeAttendancesByDateAsync(request.EmployeeId, request.FromDate, request.ToDate);
                var Attendance_Mapped = mapper.Map<List<GetAttendancesForEmpliyeeByDateResponse>>(Attendance);
                return Success(Attendance_Mapped);
            }

            // 3. Employee can see only his own attendance
            if (currentUserService.IsInRole(Roles.Employee))
            {
                if (currentUserService.EmployeeId != request.EmployeeId)
                    return Forbidden<List<GetAttendancesForEmpliyeeByDateResponse>>("You can only view your own attendance.");

                var Attendance = await attendanceService
                .GetEmployeeAttendancesByDateAsync(request.EmployeeId, request.FromDate, request.ToDate);
                var Attendance_Mapped = mapper.Map<List<GetAttendancesForEmpliyeeByDateResponse>>(Attendance);
                return Success(Attendance_Mapped);
            }

            // 4. Manager can see only his subordinates
            if (currentUserService.IsInRole(Roles.Manager))
            {
                var currentEmployeeId = currentUserService.EmployeeId;

                if (!currentEmployeeId.HasValue)
                    return Unauthorized<List<GetAttendancesForEmpliyeeByDateResponse>>();

                var isSubordinate =
                    await employeeService.IsEmployeeUnderManagerAsync(
                        request.EmployeeId,
                        currentEmployeeId.Value);

                if (!isSubordinate)
                    return Forbidden<List<GetAttendancesForEmpliyeeByDateResponse>>("You can only view your subordinates' attendance.");

                var Attendance = await attendanceService
                .GetEmployeeAttendancesByDateAsync(request.EmployeeId, request.FromDate, request.ToDate);
                var Attendance_Mapped = mapper.Map<List<GetAttendancesForEmpliyeeByDateResponse>>(Attendance);
                return Success(Attendance_Mapped);
            }
            // 5. Any other role
            return Unauthorized<List<GetAttendancesForEmpliyeeByDateResponse>>();

        }
    }
}

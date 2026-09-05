using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Attendances.Queries.Models;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
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

        public AttendanceQueryHandler(IAttendanceService attendanceService,
            IEmployeeService employeeService,
            IMapper mapper,IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.attendanceService = attendanceService;
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.localizer = localizer;
        }

        public async Task<Response<List<GetAttendancesForEmpliyeeResponse>>> Handle(GetAttendancesForEmpliyeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetAttendancesForEmpliyeeResponse>>();
            var Attendance = await attendanceService.GetEmployeeAttendancesAsync(request.EmployeeId);
            var Attendance_Mapped = mapper.Map<List<GetAttendancesForEmpliyeeResponse>>(Attendance);
            return Success(Attendance_Mapped);
        }

        public async Task<Response<List<GetAttendancesForEmpliyeeByDateResponse>>> Handle(GetAttendancesForEmpliyeeByDateQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetAttendancesForEmpliyeeByDateResponse>>();
            var Attendance = await attendanceService
                .GetEmployeeAttendancesByDateAsync(request.EmployeeId,request.FromDate,request.ToDate);
            var Attendance_Mapped = mapper.Map<List<GetAttendancesForEmpliyeeByDateResponse>>(Attendance);
            return Success(Attendance_Mapped);
        }
    }
}

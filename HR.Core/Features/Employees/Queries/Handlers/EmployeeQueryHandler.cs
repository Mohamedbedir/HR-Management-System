using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.Employees.Queries.Models;
using HR.Core.Features.Employees.Queries.Responses;
using HR.Data.AppMetaData;
using HR.Data.Entities;
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

namespace HR.Core.Features.Employees.Queries.Handlers
{
    public class EmployeeQueryHandler : ResponseHandler,
        IRequestHandler<GetEmployeeByIdQuery, Response<GetEmployeeByIdResponse>>,
        IRequestHandler<GetEmployeesQuery, Response<IReadOnlyList<GetEmployeesResponse>>>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;

        public EmployeeQueryHandler(IStringLocalizer<SharedResources> localizer,
            IEmployeeService employeeService,
            IMapper mapper,
            ICurrentUserService currentUserService) : base(localizer)
        {
            this.localizer = localizer;
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.currentUserService = currentUserService;
        }

        public async Task<Response<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id ?? currentUserService.EmployeeId;
            if (!id.HasValue)
                return BadRequest<GetEmployeeByIdResponse>();

            var Emp = await employeeService.GetEmployeeByIdAsync(id.Value);
            if (Emp == null) 
                return NotFound<GetEmployeeByIdResponse>();
            

            // 2. Admin and HR can see any employee's attendance
            if (currentUserService.IsInRole(Roles.Admin) ||
                currentUserService.IsInRole(Roles.HR))
            {
                var Emp_Mapped = mapper.Map<GetEmployeeByIdResponse>(Emp);
                return Success(Emp_Mapped);
            }

            // 3. Any user should always be able to view their own record (even if they have multiple roles)
            if (currentUserService.EmployeeId.HasValue && id == currentUserService.EmployeeId)
            {
                var Emp_Mapped = mapper.Map<GetEmployeeByIdResponse>(Emp);
                return Success(Emp_Mapped);
            }

            // 4. Manager can see only his subordinates
            if (currentUserService.IsInRole(Roles.Manager))
            {
                var currentEmployeeId = currentUserService.EmployeeId;

                if (!currentEmployeeId.HasValue)
                    return Unauthorized<GetEmployeeByIdResponse>();

                var isSubordinate =
                    await employeeService.IsEmployeeUnderManagerAsync(
                        id.Value,
                        currentEmployeeId.Value);

                if (!isSubordinate)
                    return Forbidden<GetEmployeeByIdResponse>("You are not allowed to view this employee's record.");

                var Emp_Mapped = mapper.Map<GetEmployeeByIdResponse>(Emp);
                return Success(Emp_Mapped);
            }

            // 5. Any other role
            return Unauthorized<GetEmployeeByIdResponse>();
        }

        public async Task<Response<IReadOnlyList<GetEmployeesResponse>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var Emps = await employeeService.GetEmployeesAsync();
            var Emps_Mapped = mapper.Map<IReadOnlyList<GetEmployeesResponse>>(Emps);
            return Success(Emps_Mapped, Meta: new { DataCount = Emps_Mapped.Count() });
        }
    }
}

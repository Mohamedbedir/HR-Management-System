using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.Employees.Queries.Models;
using HR.Core.Features.Employees.Queries.Responses;
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

        public EmployeeQueryHandler(IStringLocalizer<SharedResources> localizer,
            IEmployeeService employeeService,
            IMapper mapper) : base(localizer)
        {
            this.localizer = localizer;
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        public async Task<Response<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var Emp = await employeeService.GetEmployeeByIdAsync(request.Id);
            if (Emp == null) 
                return NotFound<GetEmployeeByIdResponse>();
            var Emp_Mapped = mapper.Map<GetEmployeeByIdResponse>(Emp);
            return Success(Emp_Mapped);
        }

        public async Task<Response<IReadOnlyList<GetEmployeesResponse>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var Emps = await employeeService.GetEmployeesAsync();
            var Emps_Mapped = mapper.Map<IReadOnlyList<GetEmployeesResponse>>(Emps);
            return Success(Emps_Mapped, Meta: new { DataCount = Emps_Mapped.Count() });
        }
    }
}

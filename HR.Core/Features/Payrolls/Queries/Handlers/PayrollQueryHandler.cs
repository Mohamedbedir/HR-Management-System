using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.LeaveRequests.Queries.Responses;
using HR.Core.Features.Payrolls.Queries.Models;
using HR.Core.Features.Payrolls.Queries.Responses;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Payrolls.Queries.Handlers
{
    public class PayrollQueryHandler : ResponseHandler,
        IRequestHandler<GetPayrollByIdQuery, Response<GetPayrollByIdResponse>>,
        IRequestHandler<GetPayrollsForEmployeeQuery, Response<List<GetPayrollsForEmployeeResponse>>>,
        IRequestHandler<GetPayrollsQuery, Response<List<GetPayrollsResponse>>>
    {
        private readonly IPayrollService payrollService;
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;

        public PayrollQueryHandler(IStringLocalizer<SharedResources> localizer,
            IPayrollService payrollService,
            IMapper mapper,
            IEmployeeService employeeService) : base(localizer)
        {
            this.payrollService = payrollService;
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        public async Task<Response<GetPayrollByIdResponse>> Handle(GetPayrollByIdQuery request, CancellationToken cancellationToken)
        {
           var payroll = await payrollService.GetByIdAsync(request.Id);
            if (payroll == null)
                return NotFound<GetPayrollByIdResponse>("Payroll not found.");
            var response = mapper.Map<GetPayrollByIdResponse>(payroll);
            return Success(response);
        }

        public async Task<Response<List<GetPayrollsResponse>>> Handle(GetPayrollsQuery request, CancellationToken cancellationToken)
        {
            var payrolls = await payrollService.GetAllAsync();
            
            var response = mapper.Map<List<GetPayrollsResponse>>(payrolls);
            return Success(response, Meta: new { DataCount = response.Count() });
        }

        public async Task<Response<List<GetPayrollsForEmployeeResponse>>> Handle(GetPayrollsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetPayrollsForEmployeeResponse>>();
            var payrolls = await payrollService.GetEmployeePayrollsAsync(request.EmployeeId);
            var payrolls_Mapped = mapper.Map<List<GetPayrollsForEmployeeResponse>>(payrolls);
            return Success(payrolls_Mapped);
        }
    }
}

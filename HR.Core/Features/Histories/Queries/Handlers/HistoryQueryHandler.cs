using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Core.Features.Histories.Queries.Models;
using HR.Core.Features.Histories.Queries.Responses;
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

namespace HR.Core.Features.Histories.Queries.Handlers
{
    public class HistoryQueryHandler : ResponseHandler,
        IRequestHandler<GetSalaryHistoryForEmployeeQuery, Response<List<GetSalaryHistoryForEmployeeResponse>>>,
        IRequestHandler<GetHistoryForEmployeeQuery,Response<List<GetHistoryForEmployeeResponse>>>
    {
        private readonly ISalaryHistoryService historyService;
        private readonly IEmploymentHistoryService employmentHistoryService;
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;

        public HistoryQueryHandler(ISalaryHistoryService historyService,
            IEmploymentHistoryService employmentHistoryService,
            IMapper mapper,
            IEmployeeService employeeService,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.historyService = historyService;
            this.employmentHistoryService = employmentHistoryService;
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        public async Task<Response<List<GetSalaryHistoryForEmployeeResponse>>> Handle(GetSalaryHistoryForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetSalaryHistoryForEmployeeResponse>>();
            var history = await historyService.GetSalaryHistoriesAsync(request.EmployeeId);
            var history_Mapped = mapper.Map<List<GetSalaryHistoryForEmployeeResponse>>(history);
            return Success(history_Mapped);
        }
        public async Task<Response<List<GetHistoryForEmployeeResponse>>> Handle(GetHistoryForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetHistoryForEmployeeResponse>>();
            var history = await employmentHistoryService.GetEmploymentHistoriesAsync(request.EmployeeId);
            var history_Mapped = mapper.Map<List<GetHistoryForEmployeeResponse>>(history);
            return Success(history_Mapped);
        }
    }
}

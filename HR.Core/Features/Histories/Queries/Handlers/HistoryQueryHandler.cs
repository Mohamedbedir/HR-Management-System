using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Core.Features.Histories.Queries.Models;
using HR.Core.Features.Histories.Queries.Responses;
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
        private readonly ICurrentUserService currentUserService;

        public HistoryQueryHandler(ISalaryHistoryService historyService,
            IEmploymentHistoryService employmentHistoryService,
            IMapper mapper,
            IEmployeeService employeeService,
            ICurrentUserService currentUserService,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.historyService = historyService;
            this.employmentHistoryService = employmentHistoryService;
            this.mapper = mapper;
            this.employeeService = employeeService;
            this.currentUserService = currentUserService;
        }

        public async Task<Response<List<GetSalaryHistoryForEmployeeResponse>>> Handle(GetSalaryHistoryForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetSalaryHistoryForEmployeeResponse>>();
            // Only Admin / HR should reach this operation
            if (!currentUserService.IsInRole(Roles.Admin) &&
                !currentUserService.IsInRole(Roles.HR))
            {
                return Forbidden<List<GetSalaryHistoryForEmployeeResponse>>("You are not allowed to view salary history.");
            }

            var history =
                await historyService.GetSalaryHistoriesAsync(
                    request.EmployeeId);

            var historyMapped =
                mapper.Map<List<GetSalaryHistoryForEmployeeResponse>>(history);

            return Success(historyMapped);
        }
        public async Task<Response<List<GetHistoryForEmployeeResponse>>> Handle(GetHistoryForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetHistoryForEmployeeResponse>>();
            // Admin / HR → can see any employee
            if (currentUserService.IsInRole(Roles.Admin) ||
                currentUserService.IsInRole(Roles.HR))
            {
                var history =
                    await employmentHistoryService
                        .GetEmploymentHistoriesAsync(request.EmployeeId);

                var mapped =
                    mapper.Map<List<GetHistoryForEmployeeResponse>>(history);

                return Success(mapped);
            }

            // Employee → can see himself only
            if (currentUserService.IsInRole(Roles.Employee))
            {
                if (currentUserService.EmployeeId != request.EmployeeId)
                    return Forbidden<List<GetHistoryForEmployeeResponse>>("You can only view your own history.");

                var history =
                    await employmentHistoryService
                        .GetEmploymentHistoriesAsync(request.EmployeeId);

                var mapped =
                    mapper.Map<List<GetHistoryForEmployeeResponse>>(history);

                return Success(mapped);
            }

            // Manager → can see his subordinates only
            if (currentUserService.IsInRole(Roles.Manager))
            {
                var managerEmployeeId = currentUserService.EmployeeId;

                if (!managerEmployeeId.HasValue)
                    return Forbidden<List<GetHistoryForEmployeeResponse>>("You are not allowed to view this employee's history.");

                var isSubordinate =
                    await employeeService.IsEmployeeUnderManagerAsync(
                        request.EmployeeId,
                        managerEmployeeId.Value);

                if (!isSubordinate)
                    return Forbidden<List<GetHistoryForEmployeeResponse>>("You can only view your subordinates' history.");

                var history =
                    await employmentHistoryService
                        .GetEmploymentHistoriesAsync(request.EmployeeId);

                var mapped =
                    mapper.Map<List<GetHistoryForEmployeeResponse>>(history);

                return Success(mapped);
            }

            return Forbidden<List<GetHistoryForEmployeeResponse>>("You are not allowed to view this employee's history.");
        }
    }
}

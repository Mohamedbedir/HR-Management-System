using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.LeaveRequests.Queries.Models;
using HR.Core.Features.LeaveRequests.Queries.Responses;
using HR.Service.Services;
using HR.Service.Services.Contract;
using MediatR;
using HR.Data.AppMetaData;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Queries.Handlers
{
    public class LeaveRequestQueryHandler : ResponseHandler,
        IRequestHandler<GetLeaveRequestByIdQuery,Response<GetLeaveRequestByIdRespose>>,
        IRequestHandler<GetLeaveRequestsQuery,Response<List<GetLeaveRequestsRespose>>>,
        IRequestHandler<GetLeaveRequestsForEmployeeQuery, Response<List<GetLeaveRequestsForEmployeeRespose>>>
    {
        private readonly ILeaveRequestService requestService;
        private readonly IEmployeeService employeeService;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public LeaveRequestQueryHandler(ILeaveRequestService requestService,
            IEmployeeService employeeService,
            ICurrentUserService currentUserService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.requestService = requestService;
            this.employeeService = employeeService;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<Response<GetLeaveRequestByIdRespose>> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = await requestService.GetByIdIncludeAsync(request.Id);
            if (leaveRequest == null)
                return NotFound<GetLeaveRequestByIdRespose>();

            // Authorization checks
            if (currentUserService.IsInRole(Roles.Admin) || currentUserService.IsInRole(Roles.HR))
            {
                var mapped = mapper.Map<GetLeaveRequestByIdRespose>(leaveRequest);
                return Success(mapped);
            }

            if (currentUserService.IsInRole(Roles.Manager))
            {
                if (!currentUserService.EmployeeId.HasValue)
                    return Forbidden<GetLeaveRequestByIdRespose>();

                var isSub = await employeeService.IsEmployeeUnderManagerAsync(leaveRequest.EmployeeId, currentUserService.EmployeeId.Value);
                if (!isSub)
                    return Forbidden<GetLeaveRequestByIdRespose>();

                var mapped = mapper.Map<GetLeaveRequestByIdRespose>(leaveRequest);
                return Success(mapped);
            }

            if (currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue || currentUserService.EmployeeId.Value != leaveRequest.EmployeeId)
                    return Forbidden<GetLeaveRequestByIdRespose>();

                var mapped = mapper.Map<GetLeaveRequestByIdRespose>(leaveRequest);
                return Success(mapped);
            }

            return Forbidden<GetLeaveRequestByIdRespose>();
        }

        public async Task<Response<List<GetLeaveRequestsRespose>>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var LeaveRequests = await requestService.GetAllAsync();
            var LeaveRequests_Mapped = mapper.Map<List<GetLeaveRequestsRespose>>(LeaveRequests);
            return Success(LeaveRequests_Mapped);
        }

        public async Task<Response<List<GetLeaveRequestsForEmployeeRespose>>> Handle(GetLeaveRequestsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetLeaveRequestsForEmployeeRespose>>();

            // Authorization
            if (currentUserService.IsInRole(Roles.Admin) || currentUserService.IsInRole(Roles.HR))
            {
                var leaveRequests = await requestService.GetEmployeeLeaveRequestsAsync(request.EmployeeId);
                var mapped = mapper.Map<List<GetLeaveRequestsForEmployeeRespose>>(leaveRequests);
                return Success(mapped);
            }

            if (currentUserService.IsInRole(Roles.Manager))
            {
                if (!currentUserService.EmployeeId.HasValue)
                    return Forbidden<List<GetLeaveRequestsForEmployeeRespose>>();

                var isSub = await employeeService.IsEmployeeUnderManagerAsync(request.EmployeeId, currentUserService.EmployeeId.Value);
                if (!isSub)
                    return Forbidden<List<GetLeaveRequestsForEmployeeRespose>>();

                var leaveRequests = await requestService.GetEmployeeLeaveRequestsAsync(request.EmployeeId);
                var mapped = mapper.Map<List<GetLeaveRequestsForEmployeeRespose>>(leaveRequests);
                return Success(mapped);
            }

            if (currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue || currentUserService.EmployeeId.Value != request.EmployeeId)
                    return Forbidden<List<GetLeaveRequestsForEmployeeRespose>>();

                var leaveRequests = await requestService.GetEmployeeLeaveRequestsAsync(request.EmployeeId);
                var mapped = mapper.Map<List<GetLeaveRequestsForEmployeeRespose>>(leaveRequests);
                return Success(mapped);
            }

            return Forbidden<List<GetLeaveRequestsForEmployeeRespose>>();
        }
    }
}

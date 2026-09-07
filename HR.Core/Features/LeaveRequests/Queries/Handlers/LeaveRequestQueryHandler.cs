using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Attendances.Queries.Responses;
using HR.Core.Features.LeaveRequests.Queries.Models;
using HR.Core.Features.LeaveRequests.Queries.Responses;
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

namespace HR.Core.Features.LeaveRequests.Queries.Handlers
{
    public class LeaveRequestQueryHandler : ResponseHandler,
        IRequestHandler<GetLeaveRequestByIdQuery,Response<GetLeaveRequestByIdRespose>>,
        IRequestHandler<GetLeaveRequestsQuery,Response<List<GetLeaveRequestsRespose>>>,
        IRequestHandler<GetLeaveRequestsForEmployeeQuery, Response<List<GetLeaveRequestsForEmployeeRespose>>>
    {
        private readonly ILeaveRequestService requestService;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;

        public LeaveRequestQueryHandler(ILeaveRequestService requestService,
            IEmployeeService employeeService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.requestService = requestService;
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        public async Task<Response<GetLeaveRequestByIdRespose>> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var LeaveRequest=await requestService.GetByIdIncludeAsync(request.Id);
            if (LeaveRequest == null)
                return NotFound<GetLeaveRequestByIdRespose>();
            var Mapped = mapper.Map<GetLeaveRequestByIdRespose>(LeaveRequest);
            return Success(Mapped);
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
            var LeaveRequests = await requestService.GetEmployeeLeaveRequestsAsync(request.EmployeeId);
            var LeaveRequests_Mapped = mapper.Map<List<GetLeaveRequestsForEmployeeRespose>>(LeaveRequests);
            return Success(LeaveRequests_Mapped);
        }
    }
}

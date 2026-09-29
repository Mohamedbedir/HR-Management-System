using HR.Core.Bases;
using HR.Core.Features.LeaveRequests.Commands.Models;
using HR.Data.Entities;
using HR.Data.Enums;
using HR.Data.AppMetaData;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Commands.Handlers
{
    public class LeaveRequestCommandHandler
    : ResponseHandler,
      IRequestHandler<CreateLeaveRequestCommand, Response<string>>,
      IRequestHandler<RejectLeaveRequestCommand, Response<string>>,
      IRequestHandler<CancelLeaveRequestCommand, Response<string>>,
      IRequestHandler<ApproveLeaveRequestCommand, Response<string>>
    {
        private readonly IEmployeeService employeeService;
        private readonly ILeaveTypeService leaveTypeService;
        private readonly ILeaveRequestService leaveRequestService;
        private readonly ICurrentUserService currentUserService;

        public LeaveRequestCommandHandler(IStringLocalizer<SharedResources> localizer,
            IEmployeeService employeeService,
            ILeaveTypeService leaveTypeService,
            ILeaveRequestService leaveRequestService,
            ICurrentUserService currentUserService) : base(localizer)
        {
            this.employeeService = employeeService;
            this.leaveTypeService = leaveTypeService;
            this.leaveRequestService = leaveRequestService;
            this.currentUserService = currentUserService;
        }

       

        public async Task<Response<string>> Handle(CreateLeaveRequestCommand request,
            CancellationToken cancellationToken)
        {
            // For Create, take EmployeeId from current user (ignore model)
            if (!currentUserService.EmployeeId.HasValue)
                return Forbidden<string>();

            int effectiveEmployeeId = currentUserService.EmployeeId.Value;

            // 1. Check Employee
            var employee = await employeeService.GetEmployeeByIdAsync(effectiveEmployeeId);

            if (employee == null)
                return NotFound<string>("Employee not found.");

            // 2. Check Employee Status
            if (employee.Status != EmployeeStatus.Active)
                return BadRequest<string>("Employee is not active.");

            // 3. Check Leave Type
            var leaveType =
                await leaveTypeService.GetLeaveTypeByIdAsync(request.LeaveTypeId);

            if (leaveType == null)
                return NotFound<string>("Leave type not found.");

            // 4. Check overlapping requests
            var isOverlapping =
                await leaveRequestService.IsOverlappingLeaveRequestAsync(
                    effectiveEmployeeId,
                    request.StartDate,
                    request.EndDate);

            if (isOverlapping)
                return Conflict<string>(
                    "Employee already has a leave request for this period.");

            // Authorization: Admin/HR can create for any; Manager can create for subordinates; Employee only for self
            if (currentUserService.IsInRole(Roles.Admin) || currentUserService.IsInRole(Roles.HR))
            {
                // allowed
            }
            else if (currentUserService.IsInRole(Roles.Manager) && !currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue)
                    return Forbidden<string>();

                var isSub = await employeeService.IsEmployeeUnderManagerAsync(effectiveEmployeeId, currentUserService.EmployeeId.Value);
                if (!isSub)
                    return Forbidden<string>();
            }
            else if (currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue || currentUserService.EmployeeId.Value != effectiveEmployeeId)
                    return Forbidden<string>();
            }
            else
            {
                return Forbidden<string>();
            }

            // 5. Calculate number of leave days
            var leaveDays =
                request.EndDate.DayNumber - request.StartDate.DayNumber + 1;

            // 6. Create Leave Request
            var leaveRequest = new LeaveRequest
            {
                EmployeeId = effectiveEmployeeId,
                LeaveTypeId = request.LeaveTypeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Reason = request.Reason,
                Status = LeaveRequestStatus.Pending,
                CreatedAt = DateTime.Now
            };

            // 7. Add
            await leaveRequestService.AddAsync(leaveRequest);

            // 8. Save
            await leaveRequestService.SaveChangesAsync();

            return Success<string>(
                "Leave request created successfully.");
        }

        public async Task<Response<string>> Handle(ApproveLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest =
            await leaveRequestService.GetByIdAsync(request.LeaveRequestId);

            
            if (leaveRequest == null)
                return NotFound<string>("Leave request not found.");

            if (leaveRequest.Status != LeaveRequestStatus.Pending)
                return BadRequest<string>(
                    "Only pending leave requests can be approved.");

            // Authorization: Admin/HR can approve any; Manager only for subordinates
            if (currentUserService.IsInRole(Roles.Admin) || currentUserService.IsInRole(Roles.HR))
            {
                // allowed
            }
            else if (currentUserService.IsInRole(Roles.Manager) && !currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue)
                    return Forbidden<string>();

                var isSub = await employeeService.IsEmployeeUnderManagerAsync(leaveRequest.EmployeeId, currentUserService.EmployeeId.Value);
                if (!isSub)
                    return Forbidden<string>();
            }
            else
            {
                return Forbidden<string>();
            }

            leaveRequest.Status = LeaveRequestStatus.Approved;
            leaveRequest.ApprovedAt = DateTime.Now;
            // set ApprovedById from current user if available
            leaveRequest.ApprovedById = currentUserService.UserId ?? 0;

            await leaveRequestService.UpdateAsync(leaveRequest);
            await leaveRequestService.SaveChangesAsync();

            return Success<string>(
                "Leave request approved successfully.");
        }

        public async Task<Response<string>> Handle(RejectLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest =
            await leaveRequestService.GetByIdAsync(request.LeaveRequestId);

            if (leaveRequest == null)
                return NotFound<string>("Leave request not found.");


            if (leaveRequest.Status != LeaveRequestStatus.Pending)
                return BadRequest<string>(
                    "Only pending leave requests can be rejected.");

            // Authorization: Admin/HR can reject any; Manager only for subordinates
            if (currentUserService.IsInRole(Roles.Admin) || currentUserService.IsInRole(Roles.HR))
            {
                // allowed
            }
            else if (currentUserService.IsInRole(Roles.Manager) && !currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue)
                    return Forbidden<string>();

                var isSub = await employeeService.IsEmployeeUnderManagerAsync(leaveRequest.EmployeeId, currentUserService.EmployeeId.Value);
                if (!isSub)
                    return Forbidden<string>();
            }
            else
            {
                return Forbidden<string>();
            }

            leaveRequest.Status = LeaveRequestStatus.Rejected;
            leaveRequest.RejectionReason = request.RejectionReason;

            await leaveRequestService.UpdateAsync(leaveRequest);
            await leaveRequestService.SaveChangesAsync();

            return Success<string>(
                "Leave request rejected successfully.");
        }

        public async Task<Response<string>> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest =
           await leaveRequestService.GetByIdAsync(request.LeaveRequestId);

            if (leaveRequest == null)
                return NotFound<string>("Leave request not found.");

            if (leaveRequest.Status != LeaveRequestStatus.Pending)
                return BadRequest<string>(
                    "Only pending leave requests can be cancelled.");

            // Authorization: Admin/HR can cancel any; Manager only subordinates; Employee only own
            if (currentUserService.IsInRole(Roles.Admin) || currentUserService.IsInRole(Roles.HR))
            {
                // allowed
            }
            else if (currentUserService.IsInRole(Roles.Manager) && !currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue)
                    return Forbidden<string>();

                var isSub = await employeeService.IsEmployeeUnderManagerAsync(leaveRequest.EmployeeId, currentUserService.EmployeeId.Value);
                if (!isSub)
                    return Forbidden<string>();
            }
            else if (currentUserService.IsInRole(Roles.Employee))
            {
                if (!currentUserService.EmployeeId.HasValue || currentUserService.EmployeeId.Value != leaveRequest.EmployeeId)
                    return Forbidden<string>();
            }
            else
            {
                return Forbidden<string>();
            }

            leaveRequest.Status = LeaveRequestStatus.Cancelled;

            await leaveRequestService.UpdateAsync(leaveRequest);
            await leaveRequestService.SaveChangesAsync();

            return Success<string>(
                "Leave request cancelled successfully.");
        }
    }
}

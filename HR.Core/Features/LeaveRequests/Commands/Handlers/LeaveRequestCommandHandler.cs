using HR.Core.Bases;
using HR.Core.Features.LeaveRequests.Commands.Models;
using HR.Data.Entities;
using HR.Data.Enums;
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

        public LeaveRequestCommandHandler(IStringLocalizer<SharedResources> localizer,
            IEmployeeService employeeService,
            ILeaveTypeService leaveTypeService,
            ILeaveRequestService leaveRequestService) : base(localizer)
        {
            this.employeeService = employeeService;
            this.leaveTypeService = leaveTypeService;
            this.leaveRequestService = leaveRequestService;
        }

       

        public async Task<Response<string>> Handle(CreateLeaveRequestCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Check Employee
            var employee =
                await employeeService.GetEmployeeByIdAsync(request.EmployeeId);

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
                    request.EmployeeId,
                    request.StartDate,
                    request.EndDate);

            if (isOverlapping)
                return Conflict<string>(
                    "Employee already has a leave request for this period.");

            // 5. Calculate number of leave days
            var leaveDays =
                request.EndDate.DayNumber - request.StartDate.DayNumber + 1;

            // 6. Create Leave Request
            var leaveRequest = new LeaveRequest
            {
                EmployeeId = request.EmployeeId,
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

            leaveRequest.Status = LeaveRequestStatus.Approved;
            leaveRequest.ApprovedAt = DateTime.Now;
            leaveRequest.ApprovedById = 7;

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

            leaveRequest.Status = LeaveRequestStatus.Cancelled;

            await leaveRequestService.UpdateAsync(leaveRequest);
            await leaveRequestService.SaveChangesAsync();

            return Success<string>(
                "Leave request cancelled successfully.");
        }
    }
}

using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.LeaveTypes.Commands.Models;
using HR.Core.Features.Positions.Commands.Models;
using HR.Data.Entities;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveTypes.Commands.Handlers
{
    public class LeaveTypeCommandHandler : ResponseHandler,
        IRequestHandler<AddLeaveTypeCommand, Response<string>>,
        IRequestHandler<EditLeaveTypeCommand, Response<string>>,
        IRequestHandler<DeleteLeaveTypeCommand, Response<string>>
    {
        private readonly ILeaveTypeService leaveTypeService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public LeaveTypeCommandHandler(ILeaveTypeService leaveTypeService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer) 
        {
            this.leaveTypeService = leaveTypeService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
        public async Task<Response<string>> Handle(AddLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeMapping = mapper.Map<LeaveType>(request);
            var res = await leaveTypeService.CreateLeaveTypeAsync(leaveTypeMapping);
            
            if (res == "Success")
                return Created<string>("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await leaveTypeService.GetLeaveTypeByIdAsync(request.Id);

            if (leaveType == null)
                return NotFound<string>();

            var resdelete = await leaveTypeService.DeleteLeaveTypeAsync(leaveType);

            if (resdelete != "Success")
                return BadRequest<string>();

            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(EditLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await leaveTypeService.GetLeaveTypeByIdAsync(request.Id);

            if (leaveType == null)
                return NotFound<string>();

            leaveType.Name = request.Name;
            leaveType.Description = request.Description;
            leaveType.IsActive = request.IsActive;
            leaveType.IsPaid = request.IsPaid;
            leaveType.MaxDaysPerYear = request.MaxDaysPerYear;

            var resUpdate = await leaveTypeService.UpdateLeaveTypeAsync(leaveType);

            if (resUpdate != "Success")
                return BadRequest<string>();

            return Updated<string>("");
        }
    }
}

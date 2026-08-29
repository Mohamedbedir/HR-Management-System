using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.LeaveTypes.Queries.Models;
using HR.Core.Features.LeaveTypes.Queries.Responses;
using HR.Core.Features.Positions.Queries.Models;
using HR.Core.Features.Positions.Queries.Responses;
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

namespace HR.Core.Features.LeaveTypes.Queries.Handlers
{
    public class LeaveTypeQueryHandler : ResponseHandler,
        IRequestHandler<GetLeaveTypeByIdQuery, Response<GetLeaveTypeByIdResponse>>,
        IRequestHandler<GetLeaveTypesQuery, Response<IReadOnlyList<GetLeaveTypesResponse>>>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly ILeaveTypeService leaveTypeService;
        private readonly IMapper mapper;

        public LeaveTypeQueryHandler(IStringLocalizer<SharedResources> localizer,
            ILeaveTypeService leaveTypeService,
            IMapper mapper ) : base(localizer)
        {
            this.localizer = localizer;
            this.leaveTypeService = leaveTypeService;
            this.mapper = mapper;
        }
  
        public async Task<Response<GetLeaveTypeByIdResponse>> Handle(GetLeaveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var type= await leaveTypeService.GetLeaveTypeByIdAsync(request.Id);

            if (type == null)
                return NotFound<GetLeaveTypeByIdResponse>();

            var type_Mapped = mapper.Map<GetLeaveTypeByIdResponse>(type);

            return Success(type_Mapped);
        }

        public async Task<Response<IReadOnlyList<GetLeaveTypesResponse>>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            var type = await leaveTypeService.GetAllLeaveTypesAsync();
            var type_Mapped = mapper.Map<IReadOnlyList<GetLeaveTypesResponse>>(type);
            return Success(type_Mapped, Meta: new { DataCount = type_Mapped.Count() });
        }
    }
}

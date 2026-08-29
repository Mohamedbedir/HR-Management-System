using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Departments.Queries.Responses;
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

namespace HR.Core.Features.Positions.Queries.Handlers
{
    public class PositionQueryHandler : ResponseHandler,
        IRequestHandler<GetPositionByIdQuery, Response<GetPositionByIdResponse>>,
        IRequestHandler<GetPositionsQuery, Response<IReadOnlyList<GetPositionsResponse>>>
    {
        private readonly IPositionService positionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public PositionQueryHandler(IPositionService positionService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.positionService = positionService;
            this.mapper = mapper;
            this.localizer = localizer;
        }

        public async Task<Response<GetPositionByIdResponse>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
        {
            var pos= await positionService.GetPositionByIdAsync(request.Id);

            if (pos == null)
                return NotFound<GetPositionByIdResponse>();

            var pos_Mapped= mapper.Map<GetPositionByIdResponse>(pos);

            return Success(pos_Mapped);
        }

        async Task<Response<IReadOnlyList<GetPositionsResponse>>> IRequestHandler<GetPositionsQuery, Response<IReadOnlyList<GetPositionsResponse>>>.Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var pos = await positionService.GetAllPositionsAsync();
            var pos_Mapped = mapper.Map<IReadOnlyList<GetPositionsResponse>>(pos);
            return Success(pos_Mapped, Meta: new { DataCount = pos_Mapped.Count() });
        }
    }
}

using HR.Core.Bases;
using HR.Core.Features.Dashboard.Queries.Models;
using HR.Service.DTOs;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Dashboard.Queries.Handlers
{
    public class DashboardStatisticsQueryHandler : ResponseHandler,
        IRequestHandler<GetDashboardStatisticsQuery,Response<DashboardStatisticsDto>>,
        IRequestHandler<GetRecentActivitiesQuery,Response<IReadOnlyList<RecentActivityDto>>>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IDashboardService _dashboardService;

        public DashboardStatisticsQueryHandler(
            IStringLocalizer<SharedResources> localizer,
            IDashboardService dashboardService):base(localizer)
        {
            this.localizer = localizer;
            _dashboardService = dashboardService;
        }

        public async Task<Response<DashboardStatisticsDto>> Handle(GetDashboardStatisticsQuery request,
            CancellationToken cancellationToken)
        {
            var statistics =
                await _dashboardService.GetStatisticsAsync(
                    cancellationToken);

            return Success(statistics,Message:"Dashboard statistics retrieved successfully.");
        }

        public async Task<Response<IReadOnlyList<RecentActivityDto>>> Handle(GetRecentActivitiesQuery request,
        CancellationToken cancellationToken)
        {
            var activities =
                await _dashboardService.GetRecentActivitiesAsync(
                    request.Count,
                    cancellationToken);

            return Success(activities,Message:"Recent activities retrieved successfully.");
        }
    }
}

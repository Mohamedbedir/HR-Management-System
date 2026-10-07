using HR.Core.Bases;
using HR.Core.Features.Applications.Queries.Responses;
using HR.Core.Features.Dashboard.Queries.Models;
using HR.Data.AppMetaData;
using HR.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;

namespace HR.API.Controllers
{
    //[Route("api/[controller]")]
    [Authorize(Roles = $"{Roles.Admin}")]
    [ApiController]
    public class DashboardController : AppControllerBase
    {
        [HttpGet(Router.DashboardRouting.Statistics)]
        [ProducesResponseType(typeof(Response<DashboardStatisticsDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<DashboardStatisticsDto>>> GetStatistics(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetDashboardStatisticsQuery(),cancellationToken);

            return NewResult(response);
        }
        [HttpGet(Router.DashboardRouting.RecentActivities)]
        [ProducesResponseType(typeof(Response<IReadOnlyList<RecentActivityDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IReadOnlyList<RecentActivityDto>>>> GetRecentActivities(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetRecentActivitiesQuery(),cancellationToken);

            return NewResult(response);
        }
    }
}

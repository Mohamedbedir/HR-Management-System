using HR.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IDashboardService
    {
        Task<DashboardStatisticsDto> GetStatisticsAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<RecentActivityDto>> GetRecentActivitiesAsync(int count = 10,
       CancellationToken cancellationToken = default);
    }
}

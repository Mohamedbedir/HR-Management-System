using HR.Core.Bases;
using HR.Service.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Dashboard.Queries.Models
{
    public class GetRecentActivitiesQuery
    : IRequest<Response<IReadOnlyList<RecentActivityDto>>>
    {
        public int Count { get; set; } = 10;
    }
}

using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.PerformanceReviews.Queries.Models;
using HR.Core.Features.PerformanceReviews.Queries.Responses;
using HR.Service.Services.Contract;
using HR.Data.AppMetaData;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.PerformanceReviews.Queries.Handlers
{
    public class PerformanceReviewQueryHandler : ResponseHandler,
        IRequestHandler<GetReviewByIdQuery, Response<GetReviewByIdResponse>>,
        IRequestHandler<GetReviewsQuery, Response<List<GetReviewsResponse>>>,
        IRequestHandler<GetReviewsForEmployeeQuery, Response<List<GetReviewsForEmployeeResponse>>>
    {
        private readonly IPerformanceReviewService reviewService;
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;
        private readonly ICurrentUserService currentUserService;

        public PerformanceReviewQueryHandler(IStringLocalizer<SharedResources> localizer,
            IPerformanceReviewService reviewService,
            IMapper mapper,
            IEmployeeService employeeService,
            ICurrentUserService currentUserService) : base(localizer)
        {
            this.reviewService = reviewService;
            this.mapper = mapper;
            this.employeeService = employeeService;
            this.currentUserService = currentUserService;
        }

        public async Task<Response<GetReviewByIdResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            // Enforce role check at handler level as well
            if (!currentUserService.IsInRole(Roles.Admin) &&
                !currentUserService.IsInRole(Roles.HR) &&
                !currentUserService.IsInRole(Roles.Manager))
            {
                return Forbidden<GetReviewByIdResponse>("Forbidden");
            }

            var rev = await reviewService.GetReviewByIdAsync(request.Id);
            if (rev == null)
                return NotFound<GetReviewByIdResponse>();

            // If requester is a Manager (but not Admin/HR), ensure they only access reviews of their subordinates
            if (currentUserService.IsInRole(Roles.Manager) &&
                !currentUserService.IsInRole(Roles.Admin) &&
                !currentUserService.IsInRole(Roles.HR))
            {
                var currentEmployeeId = currentUserService.EmployeeId;
                if (!currentEmployeeId.HasValue)
                    return Forbidden<GetReviewByIdResponse>("Forbidden");

                if (rev.Employee?.ManagerId != currentEmployeeId.Value)
                    return Forbidden<GetReviewByIdResponse>("Forbidden");
            }
            var rev_Mapp = mapper.Map<GetReviewByIdResponse>(rev);
            return Success(rev_Mapp);
        }

        public async Task<Response<List<GetReviewsResponse>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var Reviews = await reviewService.GetAllReviewsAsync();
            var Mapped = mapper.Map<List<GetReviewsResponse>>(Reviews);
            return Success(Mapped, Meta: Mapped.Count());
        }

        public async Task<Response<List<GetReviewsForEmployeeResponse>>> Handle(GetReviewsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            // Enforce role check at handler level as well
            if (!currentUserService.IsInRole(Roles.Admin) &&
                !currentUserService.IsInRole(Roles.HR) &&
                !currentUserService.IsInRole(Roles.Manager))
            {
                return Forbidden<List<GetReviewsForEmployeeResponse>>("Forbidden");
            }

            // If requester is a Manager (but not Admin/HR), ensure they only access reviews of their subordinates
            if (currentUserService.IsInRole(Roles.Manager) &&
                !currentUserService.IsInRole(Roles.Admin) &&
                !currentUserService.IsInRole(Roles.HR))
            {
                var currentEmployeeId = currentUserService.EmployeeId;
                if (!currentEmployeeId.HasValue)
                    return Forbidden<List<GetReviewsForEmployeeResponse>>("Forbidden");

                var requestedEmployee = await employeeService.GetEmployeeByIdAsync(request.EmployeeId);
                if (requestedEmployee == null)
                    return NotFound<List<GetReviewsForEmployeeResponse>>();

                if (requestedEmployee.ManagerId != currentEmployeeId.Value)
                    return Forbidden<List<GetReviewsForEmployeeResponse>>("Forbidden");
            }

            var EmpExists = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!EmpExists)
                return NotFound<List<GetReviewsForEmployeeResponse>>();
            var Reviews= await reviewService.GetEmployeeReviewsAsync(request.EmployeeId);
            var rev_Mapp = mapper.Map<List<GetReviewsForEmployeeResponse>>(Reviews);
            return Success(rev_Mapp,rev_Mapp.Count());
        }
    }
}

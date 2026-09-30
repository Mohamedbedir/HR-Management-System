using HR.Core.Bases;
using HR.Core.Features.PerformanceReviews.Commands.Models;
using HR.Data.Entities;
using HR.Data.Enums;
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

namespace HR.Core.Features.PerformanceReviews.Commands.Handlers
{
    public class PerformanceReviewCommandHandler
    : ResponseHandler,
      IRequestHandler<CreatePerformanceReviewCommand, Response<string>>
    {
        private readonly IPerformanceReviewService performanceReviewService;
        private readonly IEmployeeService employeeService;
        private readonly ICurrentUserService currentUserService;

        public PerformanceReviewCommandHandler(IStringLocalizer<SharedResources> localizer,
            IPerformanceReviewService performanceReviewService,
            IEmployeeService employeeService,
            ICurrentUserService currentUserService):base(localizer)
        {
            this.performanceReviewService = performanceReviewService;
            this.employeeService = employeeService;
            this.currentUserService = currentUserService;
        }

        public async Task<Response<string>> Handle(
            CreatePerformanceReviewCommand request,
            CancellationToken cancellationToken)
        {
            // ==========================================
            // Current Logged-in Employee
            // ==========================================

            var currentEmployeeId = currentUserService.EmployeeId;

            //if (!currentEmployeeId.HasValue)
            //    return Forbidden<string>("You are not linked to an employee.");

            // ==========================================
            // Employee
            // ==========================================

            var employee =
                await employeeService.GetEmployeeByIdAsync(
                    request.EmployeeId);

            if (employee == null)
                return NotFound<string>(
                    "Employee not found.");

            if (employee.Status != EmployeeStatus.Active)
                return BadRequest<string>(
                    "Only active employees can be reviewed.");

            // ==========================================
            // Reviewer
            // ==========================================

            var reviewer =
                await employeeService.GetEmployeeByIdAsync(
                    currentEmployeeId.Value);

            if (reviewer == null)
                return NotFound<string>(
                    "Reviewer not found.");

            if (reviewer.Status != EmployeeStatus.Active)
                return BadRequest<string>(
                    "Reviewer must be an active employee.");

            // ==========================================
            // Reviewer must be the employee's manager
            // ==========================================

            if (employee.ManagerId != currentEmployeeId.Value)
                return Forbidden<string>("You can only review your own subordinates.");

            // ==========================================
            // Check duplicate review
            // ==========================================

            var reviewExists =
                await performanceReviewService
                    .IsReviewExistForMonthAsync(
                        request.EmployeeId,
                        request.Month,
                        request.Year);

            if (reviewExists)
                return Conflict<string>(
                    "Performance review already exists for this employee and month.");

            // ==========================================
            // Create Review
            // ==========================================

            var review = new PerformanceReview
            {
                EmployeeId = request.EmployeeId,

                // Reviewer comes from JWT
                ReviewerId = currentEmployeeId.Value,

                Month = request.Month,
                Year = request.Year,
                Score = request.Score,
                Comments = request.Comments,

                ReviewDate = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            await performanceReviewService.AddReveiwAsync(review);

            await performanceReviewService.SavechangesAsync();

            return Success<string>(entity:"",Message:
                "Performance review created successfully.");
        }
    }
}

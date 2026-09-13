using HR.Core.Bases;
using HR.Core.Features.PerformanceReviews.Commands.Models;
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

namespace HR.Core.Features.PerformanceReviews.Commands.Handlers
{
    public class PerformanceReviewCommandHandler
    : ResponseHandler,
      IRequestHandler<CreatePerformanceReviewCommand, Response<string>>
    {
        private readonly IPerformanceReviewService performanceReviewService;
        private readonly IEmployeeService employeeService;

        public PerformanceReviewCommandHandler(IStringLocalizer<SharedResources> localizer,
            IPerformanceReviewService performanceReviewService,
            IEmployeeService employeeService):base(localizer)
        {
            this.performanceReviewService = performanceReviewService;
            this.employeeService = employeeService;
        }

        public async Task<Response<string>> Handle(
            CreatePerformanceReviewCommand request,
            CancellationToken cancellationToken)
        {
            // Employee
            var employee =
                await employeeService.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null)
                return NotFound<string>("Employee not found.");

            if (employee.Status != EmployeeStatus.Active)
                return BadRequest<string>(
                    "Only active employees can be reviewed.");

            // Reviewer
            var reviewer =
                await employeeService.GetEmployeeByIdAsync(request.ReviewerId);

            if (reviewer == null)
                return NotFound<string>("Reviewer not found.");

            if (reviewer.Status != EmployeeStatus.Active)
                return BadRequest<string>(
                    "Reviewer must be an active employee.");

            // Reviewer must be the employee's manager
            if (employee.ManagerId != request.ReviewerId)
                return BadRequest<string>(
                    "Reviewer must be the employee's manager.");

            // Check duplicate review
            var reviewExists =await performanceReviewService
                .IsReviewExistForMonthAsync(request.EmployeeId,request.Month, request.Year);

            if (reviewExists)
                return Conflict<string>(
                    "Performance review already exists for this employee and month.");

            // Create review
            var review = new PerformanceReview
            {
                EmployeeId = request.EmployeeId,
                ReviewerId = request.ReviewerId,
                Month = request.Month,
                Year = request.Year,
                Score = request.Score,
                Comments = request.Comments,
                ReviewDate = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            await performanceReviewService.AddReveiwAsync(review);
            await performanceReviewService.SavechangesAsync();

            return Success<string>(
                "Performance review created successfully.");
        }
    }
}

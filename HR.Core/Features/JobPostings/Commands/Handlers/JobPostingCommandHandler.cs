using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.JobPostings.Commands.Models;
using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
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

namespace HR.Core.Features.JobPostings.Commands.Handlers
{
    public class JobPostingCommandHandler : ResponseHandler,
        IRequestHandler<AddJobPostingCommand, Response<string>>,
        IRequestHandler<EditJobPostingCommand, Response<string>>,
        IRequestHandler<DeleteJobPostingCommand, Response<string>>
    {
        private readonly IDepartmentService departmentService;
        private readonly IPositionService positionService;
        private readonly IJobPostingService jobPostingService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public JobPostingCommandHandler(IDepartmentService departmentService,
            IPositionService positionService,
            IJobPostingService jobPostingService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer) 
        {
            this.departmentService = departmentService;
            this.positionService = positionService;
            this.jobPostingService = jobPostingService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
        

        
        



        public async Task<Response<string>> Handle(
        AddJobPostingCommand request,
        CancellationToken cancellationToken)
        {
           

            var jobPosting = new JobPosting
            {
                Title = request.Title,
                Description = request.Description,
                MinSalary = request.MinSalary,
                MaxSalary = request.MaxSalary,
                ClosingDate = request.ClosingDate,

                DepartmentId = request.DepartmentId,
                PositionId = request.PositionId,

                Status = JobPostingStatus.Published,
                PostedAt = DateTime.Now
            };

            await jobPostingService.CreateJobPostingAsync(jobPosting);

            return Success<string>(
                "Job posting created successfully.");
        }

        public async Task<Response<string>> Handle(EditJobPostingCommand request, CancellationToken cancellationToken)
        {
            // Check Job Posting
            var jobPosting =
                await jobPostingService.GetJobPostingByIdAsync(request.Id);

            if (jobPosting == null)
                return NotFound<string>("Job posting not found.");

           

            // Update
            jobPosting.Title = request.Title;
            jobPosting.Description = request.Description;
            jobPosting.MinSalary = request.MinSalary;
            jobPosting.MaxSalary = request.MaxSalary;
            jobPosting.ClosingDate = request.ClosingDate;
            jobPosting.DepartmentId = request.DepartmentId;
            jobPosting.PositionId = request.PositionId;

            await jobPostingService.UpdateJobPostingAsync(jobPosting);

            return Success<string>(
                "Job posting updated successfully.");
        }

        public async Task<Response<string>> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            // Check Job Posting
            var jobPosting =
                await jobPostingService.GetJobPostingByIdAsync(request.Id);

            if (jobPosting == null)
                return NotFound<string>("Job posting not found.");

            var result = await jobPostingService.DeleteJobPostingAsync(jobPosting);
            if (result != "Success")
                return BadRequest<string>(result);

            return Success<string>(
                "Job posting deleted successfully.");
        }
    }
}

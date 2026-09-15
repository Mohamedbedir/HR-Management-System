using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Applications.Commands.Models;
using HR.Core.Features.Candidates.Commands.Models;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.JobPostings.Commands.Models;
using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
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

namespace HR.Core.Features.Candidates.Commands.Handlers
{
    public class CandidateCommandHandler : ResponseHandler,
        IRequestHandler<AddCandidateCommand, Response<string>>,
        IRequestHandler<EditCandidateCommand, Response<string>>,
        IRequestHandler<DeleteCandidateCommand, Response<string>>
    {
        private readonly ICandidateService candidateService;
        private readonly IJobPostingService jobPostingService;
        private readonly IMapper mapper;
        private readonly IFileService fileService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public CandidateCommandHandler(ICandidateService candidateService,
            IJobPostingService jobPostingService,
            IMapper mapper,
            IFileService fileService,
            IStringLocalizer<SharedResources> localizer):base(localizer) 
        {
            this.candidateService = candidateService;
            this.jobPostingService = jobPostingService;
            this.mapper = mapper;
            this.fileService = fileService;
            this.localizer = localizer;
        }
        

        
        public async Task<Response<string>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate =
        await candidateService.GetByIdAsync(request.Id);

            if (candidate == null)
                return NotFound<string>("Candidate not found.");

            var hasApplications =
                await candidateService.HasApplicationsAsync(request.Id);

            if (hasApplications)
            {
                return Conflict<string>(
                    "Candidate cannot be deleted because they have applications.");
            }

            var cvPath = candidate.CVPath;

            await candidateService.DeleteAsync(candidate);
            await candidateService.SaveChangesAsync();

            if (!string.IsNullOrEmpty(cvPath))
            {
                await fileService.DeleteFileAsync(cvPath);
            }

            return Success<string>(
                "Candidate deleted successfully.");
        }

        public async Task<Response<string>> Handle(EditCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate =
        await candidateService.GetByIdAsync(request.Id);

            if (candidate == null)
                return NotFound<string>("Candidate not found.");

            var emailExists =
                await candidateService.IsEmailExistsForOtherCandidateAsync(
                    request.Email,
                    request.Id);

            if (emailExists)
            {
                return Conflict<string>(
                    "A candidate with this email already exists.");
            }

            var oldCVPath = candidate.CVPath;

            candidate.FirstName = request.FirstName;
            candidate.LastName = request.LastName;
            candidate.Email = request.Email;
            candidate.Phone = request.Phone;

            if (request.CV != null)
            {
                var newCVPath = await fileService.UploadFileAsync(
                    request.CV,
                    "Candidates_Cvs");

                candidate.CVPath = newCVPath;
            }

            await candidateService.UpdateAsync(candidate);
            await candidateService.SaveChangesAsync();

            if (request.CV != null && !string.IsNullOrEmpty(oldCVPath))
            {
                await fileService.DeleteFileAsync(oldCVPath);
            }

            return Success<string>(
                "Candidate updated successfully.");
        }

        public async Task<Response<string>> Handle(AddCandidateCommand request, CancellationToken cancellationToken)
        {
            var emailExists =
           await candidateService.IsEmailExistsAsync(request.Email);

            if (emailExists)
            {
                return Conflict<string>(
                    "A candidate with this email already exists.");
            }

            string? cvPath = null;

            if (request.CV != null)
            {
                cvPath = await fileService.UploadFileAsync(
                    request.CV,
                    "Candidates_Cvs");
            }

            var candidate = new Candidate
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                CVPath = cvPath,
                CreatedAt = DateTime.Now
            };

            await candidateService.AddAsync(candidate);
            await candidateService.SaveChangesAsync();

            return Success<string>(
                "Candidate created successfully.");
        }
    }
}

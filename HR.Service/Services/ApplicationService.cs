using HR.Data.Entities.Recruitment;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepo applicationRepo;

        public ApplicationService(IApplicationRepo applicationRepo)
        {
            this.applicationRepo = applicationRepo;
        }
        public async Task<string> CreateApplicationAsync(Application Application)
        {
            await applicationRepo.AddAsync(Application);
            await applicationRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> DeleteApplicationAsync(Application Application)
        {
            applicationRepo.DeleteAsync(Application);
            await applicationRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<IReadOnlyList<Application>> GetAllApplicationsAsync()
        {
            return await applicationRepo.GetTableNoTracking()
                .Include(s => s.Candidate)
                .Include(s => s.JobPosting)
                .ToListAsync();
        }

        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            return await applicationRepo.GetTableNoTracking()
                .Include(s => s.Candidate)
                .Include(s => s.JobPosting)
                .FirstOrDefaultAsync(s=>s.Id==id);
        }

        public Task<bool> IsApplicationExist(string Title)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsApplicationExistById(int id)
        {
            return await applicationRepo.AnyAsync(s=>s.Id==id);
        }

        public async Task<bool> IsApplicationExistsAsync(int JobPostingId, int CandidateId)
        {
            var existingApplication = await applicationRepo.
                AnyAsync(s => s.JobPostingId == JobPostingId && s.CandidateId == CandidateId);
            if (existingApplication)
            {
                return true;
            }
            return false;
        }

        public async Task<string> UpdateApplicationAsync(Application Application)
        {
            applicationRepo.UpdateAsync(Application);
            await applicationRepo.SaveChangesAsync();
            return "Success";
        }
    }
}

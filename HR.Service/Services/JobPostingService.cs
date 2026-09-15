using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
using HR.Infrastructure.Repositories;
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
    public class JobPostingService : IJobPostingService
    {
        private readonly IJobPostingRepo jobPostingRepo;

        public JobPostingService(IJobPostingRepo jobPostingRepo)
        {
            this.jobPostingRepo = jobPostingRepo;
        }
        public async Task<string> CreateJobPostingAsync(JobPosting JobPosting)
        {
            await jobPostingRepo.AddAsync(JobPosting);
            await jobPostingRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> DeleteJobPostingAsync(JobPosting JobPosting)
        {
            
            try
            {
                jobPostingRepo.DeleteAsync(JobPosting);
                await jobPostingRepo.SaveChangesAsync();
                return "Success";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains(
                        "REFERENCE constraint",
                        StringComparison.OrdinalIgnoreCase) == true)
                {
                    throw new Exception(
                        "Cannot delete this JobPost because it is assigned to other records.");
                }

                throw;
            }
        }

        public async Task<IReadOnlyList<JobPosting>> GetAllJobPostingsAsync()
        {
            var jobs = await jobPostingRepo.GetTableNoTracking()
                .Include(s => s.Department)
                .Include(s => s.Position)
                .ToListAsync();
            return jobs;
        }

        public async Task<JobPosting?> GetJobPostingByIdAsync(int id)
        {
            var job= await jobPostingRepo.GetTableNoTracking()
                .Include(s=>s.Department)
                .Include(s => s.Position)
                .Include(s=>s.Applications)
                .FirstOrDefaultAsync(j=>j.Id==id);
            return job;
        }

        public async Task<bool> IsJobPostingExist(string Title)
        {
            return await jobPostingRepo.AnyAsync(j => j.Title==Title);  
        }

        public async Task<bool> IsJobPostingExistById(int id)
        {
            return await jobPostingRepo.AnyAsync(j => j.Id == id);
        }

        public async Task<bool> IsJobPostingExistExcludeSelf(string name, int id)
        {
            var job = await jobPostingRepo.GetTableNoTracking()
                .Where(d => d.Title == name && !d.Id.Equals(id)).FirstOrDefaultAsync();
            if (job == null)
                return false;
            return true;
        }

        public async Task<string> UpdateJobPostingAsync(JobPosting JobPosting)
        {
            jobPostingRepo.UpdateAsync(JobPosting);
            await jobPostingRepo.SaveChangesAsync();
            return "Success";
        }
    }
}

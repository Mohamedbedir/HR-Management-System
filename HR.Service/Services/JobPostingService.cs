using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
using HR.Infrastructure.Repositories;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class JobPostingService : IJobPostingService
    {
        private readonly IJobPostingRepo jobPostingRepo;
        private readonly IDistributedCache cache;

        private const string AllJobPostingsCacheKey = "JobPostings:all";

        public JobPostingService(IJobPostingRepo jobPostingRepo, IDistributedCache cache)
        {
            this.jobPostingRepo = jobPostingRepo;
            this.cache = cache;
        }
        public async Task<string> CreateJobPostingAsync(JobPosting JobPosting)
        {
            await jobPostingRepo.AddAsync(JobPosting);
            await jobPostingRepo.SaveChangesAsync();
            await cache.RemoveAsync(AllJobPostingsCacheKey);
            return "Success";
        }

        public async Task<string> DeleteJobPostingAsync(JobPosting JobPosting)
        {
            
            try
            {
                jobPostingRepo.DeleteAsync(JobPosting);
                await jobPostingRepo.SaveChangesAsync();
                await cache.RemoveAsync(AllJobPostingsCacheKey);
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
            var cachedData = await cache.GetStringAsync(AllJobPostingsCacheKey);
            if(cachedData != null)
            {
                return JsonSerializer.Deserialize<IReadOnlyList<JobPosting>>(cachedData)!;
            }

            var jobs = await jobPostingRepo.GetTableNoTracking()
                .Include(s => s.Department)
                .Include(s => s.Position)
                .ToListAsync();

            var json = JsonSerializer.Serialize(jobs);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await cache.SetStringAsync(AllJobPostingsCacheKey, json, options);

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
            await cache.RemoveAsync(AllJobPostingsCacheKey);
            return "Success";
        }
    }
}

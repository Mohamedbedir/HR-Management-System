using HR.Data.Entities.Recruitment;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepo applicationRepo;
        private readonly IDistributedCache cache;
        private const string AllApplicationsCacheKey = "applications:all";

        public ApplicationService(IApplicationRepo applicationRepo,IDistributedCache cache)
        {
            this.applicationRepo = applicationRepo;
            this.cache = cache;
        }
        public async Task<string> CreateApplicationAsync(Application Application)
        {
            await applicationRepo.AddAsync(Application);
            await applicationRepo.SaveChangesAsync();
            await cache.RemoveAsync(AllApplicationsCacheKey);
            return "Success";
        }

        public async Task<string> DeleteApplicationAsync(Application Application)
        {
            applicationRepo.DeleteAsync(Application);
            await applicationRepo.SaveChangesAsync();
            await cache.RemoveAsync(AllApplicationsCacheKey);
            return "Success";
        }

        public async Task<IReadOnlyList<Application>> GetAllApplicationsAsync()
        {
            var cachedData = await cache.GetStringAsync(AllApplicationsCacheKey);
            var optionsForJsonser = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            if (cachedData is not null)
            {
                //cach hit
                return JsonSerializer.Deserialize<IReadOnlyList<Application>>(cachedData,optionsForJsonser)!;
            }

            var applications= await applicationRepo.GetTableNoTracking()
                .Include(s => s.Candidate)
                .Include(s => s.JobPosting)
                .ToListAsync();
            
            var json = JsonSerializer.Serialize(applications,optionsForJsonser);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await cache.SetStringAsync(AllApplicationsCacheKey, json, options);

            return applications;
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
            await cache.RemoveAsync(AllApplicationsCacheKey);
            return "Success";
        }
    }
}

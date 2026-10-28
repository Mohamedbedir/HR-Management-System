using HR.Data.Entities.Recruitment;
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
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepo candidateRepo;
        private readonly IDistributedCache cache;

        private const string AllCandidatesCacheKey = "Candidates:all";

        public CandidateService(ICandidateRepo candidateRepo,IDistributedCache cache)
        {
            this.candidateRepo = candidateRepo;
            this.cache = cache;
        }

        public async Task<Candidate?> GetByIdAsync(int id)
        {
            return await candidateRepo
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await candidateRepo
                .GetTableNoTracking()
                .AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsEmailExistsForOtherCandidateAsync(
            string email,
            int candidateId)
        {
            return await candidateRepo
                .GetTableNoTracking()
                .AnyAsync(x =>
                    x.Email == email &&
                    x.Id != candidateId);
        }

        public async Task<bool> HasApplicationsAsync(int candidateId)
        {
            return await candidateRepo
                .GetTableNoTracking()
                .AnyAsync(x =>
                    x.Id == candidateId &&
                    x.Applications.Any());
        }

        public async Task AddAsync(Candidate candidate)
        {
            await candidateRepo.AddAsync(candidate);
        }

        public async Task UpdateAsync(Candidate candidate)
        {
             candidateRepo.UpdateAsync(candidate);
        }

        public async Task DeleteAsync(Candidate candidate)
        {
             candidateRepo.DeleteAsync(candidate);
        }

        public async Task SaveChangesAsync()
        {
            await candidateRepo.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Candidate>> GetAllAsync()
        {
            var cachedData = await cache.GetStringAsync(AllCandidatesCacheKey);
            if (cachedData is not null)
            {
                //cach hit
                return JsonSerializer.Deserialize<IReadOnlyList<Candidate>>(cachedData)!;
            }
            //cach miss
            var candidte= await candidateRepo.GetTableNoTracking().ToListAsync();

            var json = JsonSerializer.Serialize(candidte);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await cache.SetStringAsync(AllCandidatesCacheKey, json, options);


            return candidte;
        }
    }
}

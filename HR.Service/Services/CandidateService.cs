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
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepo candidateRepo;

        public CandidateService(ICandidateRepo candidateRepo)
        {
            this.candidateRepo = candidateRepo;
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
            return await candidateRepo
                .GetTableNoTracking()
                .ToListAsync();
        }
    }
}

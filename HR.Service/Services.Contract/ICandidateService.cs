using HR.Data.Entities.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface ICandidateService
    {
        Task<Candidate?> GetByIdAsync(int id);
        Task<IReadOnlyList<Candidate>> GetAllAsync();

        Task<bool> IsEmailExistsAsync(string email);

        Task<bool> IsEmailExistsForOtherCandidateAsync(
            string email,
            int candidateId);

        Task<bool> HasApplicationsAsync(int candidateId);

        Task AddAsync(Candidate candidate);

        Task UpdateAsync(Candidate candidate);

        Task DeleteAsync(Candidate candidate);

        Task SaveChangesAsync();
    }
}

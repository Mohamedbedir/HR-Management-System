using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IJobPostingService
    {
        Task<JobPosting?> GetJobPostingByIdAsync(int id);

        Task<IReadOnlyList<JobPosting>> GetAllJobPostingsAsync();

        Task<string> CreateJobPostingAsync(JobPosting JobPosting);

        Task<string> UpdateJobPostingAsync(JobPosting JobPosting);

        Task<string> DeleteJobPostingAsync(JobPosting JobPosting);
        Task<bool> IsJobPostingExist(string Title);
        Task<bool> IsJobPostingExistById(int id);

        Task<bool> IsJobPostingExistExcludeSelf(string name, int id);
    }
}

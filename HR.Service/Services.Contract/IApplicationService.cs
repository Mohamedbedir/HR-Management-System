using HR.Data.Entities.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IApplicationService
    {
        Task<Application?> GetApplicationByIdAsync(int id);

        Task<IReadOnlyList<Application>> GetAllApplicationsAsync();

        Task<string> CreateApplicationAsync(Application Application);

        Task<string> UpdateApplicationAsync(Application Application);

        Task<string> DeleteApplicationAsync(Application Application);
        Task<bool> IsApplicationExist(string Title);
        Task<bool> IsApplicationExistById(int id);
        Task<bool> IsApplicationExistsAsync(int JobPostingId, int CandidateId);

        //Task<bool> IsApplicationExistExcludeSelf(string name, int id);
    }
}

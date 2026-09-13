using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IPerformanceReviewService
    {
        Task<PerformanceReview?> GetReviewByIdAsync(int id);
        Task<List<PerformanceReview>> GetEmployeeReviewsAsync(int emplyeeid);
        Task<List<PerformanceReview>> GetAllReviewsAsync();
        Task<string> AddReveiwAsync(PerformanceReview review);
        Task<bool> IsReviewExistForMonthAsync(int employeeId,int month,int year);
        Task SavechangesAsync();
    }
}

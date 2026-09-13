using HR.Data.Entities;
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
    public class PerformanceReviewService : IPerformanceReviewService
    {
        private readonly IPerformanceReviewRepo reviewRepo;

        public PerformanceReviewService(IPerformanceReviewRepo reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }
        public async Task<bool> IsReviewExistForMonthAsync(int employeeId,int month,int year)
        {
            return await reviewRepo.GetTableNoTracking()
                .AnyAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.Month == month &&
                    x.Year == year);
        }

        public async Task<string> AddReveiwAsync(PerformanceReview review)
        {
            await reviewRepo.AddAsync(review);
            return "Success";
        }

        public async Task<PerformanceReview?> GetReviewByIdAsync(int id)
        {
            return await reviewRepo
                .GetTableNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Reviewer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PerformanceReview>> GetEmployeeReviewsAsync(
            int employeeId)
        {
            return await reviewRepo
                .GetTableNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Reviewer)
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();
        }

        public async Task<List<PerformanceReview>> GetAllReviewsAsync()
        {
            return await reviewRepo
                .GetTableNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Reviewer)
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();
        }

        public async Task SavechangesAsync()
        {
            await reviewRepo.SaveChangesAsync();
        }
    }
}

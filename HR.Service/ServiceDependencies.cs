using HR.Service.Services;
using HR.Service.Services.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Service
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeDocumentService, EmployeeDocumentService>();
            services.AddScoped<IEmploymentHistoryService, EmploymentHistoryService>();
            services.AddScoped<ISalaryHistoryService, SalaryHistoryService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();
<<<<<<< Updated upstream
=======
            services.AddScoped<IPayrollService, PayrollService>();
            services.AddScoped<IPayrollCalculationService, PayrollCalculationService>();
            services.AddScoped<IPerformanceReviewService, PerformanceReviewService>();
>>>>>>> Stashed changes

            services.AddTransient<IFileService, FileService>();
            return services;
        }

    }
}

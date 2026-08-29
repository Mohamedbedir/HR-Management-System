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
            return services;
        }

    }
}

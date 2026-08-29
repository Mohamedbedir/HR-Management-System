using HR.Infrastructure.Repositories;
using HR.Infrastructure.Repositories.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IPositionRepo, PositionRepo>();
            services.AddScoped<ILeaveTypeRepo, LeaveTypeRepo>();
           //services.AddScoped(typeof(IGenericRepos<>), typeof(GenericRepos<>)); // unitofWork

            ////views
            //services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            ////Procedure
            //services.AddTransient<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();

            ////functions
            //services.AddTransient<IInstructorFunctionsRepository, InstructorFunctionsRepository>();

            return services;
        }

    }
}

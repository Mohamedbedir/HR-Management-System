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
            services.AddTransient(typeof(IGenericRepos<>), typeof(GenericRepos<>));

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

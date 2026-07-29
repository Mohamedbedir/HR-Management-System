using Microsoft.Extensions.DependencyInjection;

namespace HR.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IStudentRepository, StudentRepository>();
            
            //services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

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

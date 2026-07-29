using Microsoft.Extensions.DependencyInjection;

namespace HR.Service
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IStudentService, StudentService>();
            return services;
        }

    }
}

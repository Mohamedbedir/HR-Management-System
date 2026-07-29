using Microsoft.Extensions.DependencyInjection;

namespace HR.Core
{
    public static class CoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IStudentService, StudentService>();
            return services;
        }   
    }
}

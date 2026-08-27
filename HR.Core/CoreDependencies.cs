using FluentValidation;
using HR.Core.Behaviors;
using HR.Core.Mapping.Departments;
using HR.Core.Mapping.Positions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.Core
{
    public static class CoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            //Configuration Of Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Configuration Of Automapper
            services.AddAutoMapper(cfg =>{}, typeof(DepartmentProfile).Assembly);
            services.AddAutoMapper(cfg =>{}, typeof(PositionProfile).Assembly);
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }   
    }
}

using FluentValidation;
using HR.Core.Behaviors;
using HR.Core.Mapping.Departments;
using HR.Core.Mapping.Employees;
using HR.Core.Mapping.Histories;
using HR.Core.Mapping.LeaveTypes;
using HR.Core.Mapping.Positions;
using HR.Core.ResolverFile;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            services.AddAutoMapper(cfg => {}, typeof(LeaveTypeProfile).Assembly);
            services.AddAutoMapper(cfg => {}, typeof(EmployeeProfile).Assembly);
            services.AddAutoMapper(cfg => {}, typeof(HistoryProfile).Assembly);

            services.AddTransient<EmpDocumentFileResolver>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }   
    }
}

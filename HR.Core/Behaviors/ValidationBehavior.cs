using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResources> localizer;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators
            ,IStringLocalizer<SharedResources> localizer)
        {
            _validators = validators;
            this.localizer = localizer;
        }

       

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var message = failures.Select(x => localizer[$"{x.PropertyName}"] + " : "+ x.ErrorMessage)./*FirstOrDefault*/ToList();

                    throw new ValidationException(failures);

                }
            }
            return await next();
        }
    }

}

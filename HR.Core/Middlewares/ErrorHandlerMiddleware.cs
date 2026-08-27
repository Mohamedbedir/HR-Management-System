using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using HR.Core.Bases;
using HR.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SchoolProject.Core.Localization;

namespace HR.Core.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<SharedResources> localizer;

        public ErrorHandlerMiddleware(RequestDelegate next,IStringLocalizer<SharedResources> localizer)
        {
            _next = next;
            this.localizer = localizer;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
                //TODO:: cover all validation errors
                switch (error)
                {
                    case UnauthorizedAccessException e:
                        // custom application error
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    //case ValidationException e:
                    //    // custom validation error
                    //    responseModel.Message = error.Message;
                    //    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    //    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    //    break;
                    case ValidationException e:

                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

                        var errors = e.Errors
                            .Select(x => $"{localizer[x.PropertyName]} : {x.ErrorMessage}").ToList();
                        //.GroupBy(x => x.PropertyName)
                        //.ToDictionary(
                        //   g => localizer[g.Key].Value, // ترجمة اسم الحقل
                        //    g => g.Select(x => x.ErrorMessage).ToList()
                        //);

                        var responseValidation = new Response<object>
                        {
                            Succeeded = false,
                            StatusCode = HttpStatusCode.UnprocessableEntity,
                            Message = "Validation Failed",
                            Errors = errors
                        };

                        var json = JsonSerializer.Serialize(responseValidation);

                        await response.WriteAsync(json);
                        return;
                    case KeyNotFoundException e:
                        // not found error
                        responseModel.Message = error.Message; ;
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case DbUpdateException e:
                        // can't update error
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case Exception e:
                        if (e.GetType().ToString() == "ApiException")
                        {
                            responseModel.Message += e.Message;
                            responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                            responseModel.StatusCode = HttpStatusCode.BadRequest;
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        responseModel.Message = e.Message;
                        responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                    default:
                        // unhandled error
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}

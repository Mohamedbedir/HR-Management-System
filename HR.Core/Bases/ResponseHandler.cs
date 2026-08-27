using HR.Core.Localization;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResources> localizer;

        public ResponseHandler(IStringLocalizer<SharedResources> localizer)
        {
            this.localizer = localizer;
        }
        public Response<T> Deleted<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message == null ? localizer[SharedResourcesKeys.Deleted] : Message
            };
        }
        //public Response<T> Success<T>(string Message=null, object Meta = null)
        //{
        //    return new Response<T>()
        //    {
        //        //Data = entity,
        //        StatusCode = System.Net.HttpStatusCode.OK,
        //        Succeeded = true,
        //        Message = localizer[SharedResourcesKeys.Added],
        //        Meta = Meta
        //    };
        //}
        public Response<T> Success<T>(T entity, object Meta = null, string Message = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message== null ? localizer[SharedResourcesKeys.Success] : Message,
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized"
            };
        }
        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? "Unprocessable Entity" : Message

            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? localizer[SharedResourcesKeys.NotFound] : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = localizer[SharedResourcesKeys.Created],
                Meta = Meta
            };
        }
        public Response<T> Updated<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = localizer[SharedResourcesKeys.Updated],
                Meta = Meta
            };
        }
    }
}

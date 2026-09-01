using AutoMapper;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.ResolverFile
{
    public class EmpDocumentFileResolver : IValueResolver<EmployeeDocument, GetEmployeeDocumentByIdResponse, string>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EmpDocumentFileResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string Resolve(EmployeeDocument source, GetEmployeeDocumentByIdResponse destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.FilePath))
                return null;

            var request = httpContextAccessor.HttpContext!.Request;

            return UriHelper.BuildAbsolute(
                request.Scheme,
                request.Host,
                request.PathBase,
                "/" + source.FilePath.TrimStart('/'));
        }
    }
}

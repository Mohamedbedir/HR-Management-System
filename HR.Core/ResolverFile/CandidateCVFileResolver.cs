using AutoMapper;
using HR.Core.Features.Candidates.Queries.Responses;
using HR.Data.Entities.Recruitment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.ResolverFile
{
    public class CandidateCVFileResolver : IValueResolver<Candidate, GetCandidateByIdResponse, string>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CandidateCVFileResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string Resolve(Candidate source, GetCandidateByIdResponse destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.CVPath))
                return null;

            var request = httpContextAccessor.HttpContext!.Request;

            return UriHelper.BuildAbsolute(
                request.Scheme,
                request.Host,
                request.PathBase,
                "/" + source.CVPath.TrimStart('/'));
        }
    }
}

using HR.Core.Bases;
using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
using HR.Data.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Candidates.Commands.Models
{
    public class AddCandidateCommand : IRequest<Response<string>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        public IFormFile? CV { get; set; }
    }
}

using HR.Data.Entities;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Candidates.Queries.Responses
{
    public class GetCandidateByIdResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? CV { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}

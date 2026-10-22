using HR.Core.Bases;
using HR.Core.Features.Users.Queries.Resposes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Queries.Models
{
    public class GetAllUsersQuery : IRequest<Response<IReadOnlyList<GetAllUsersResponse>>>
    {
        public string? searsh { get; set; }
    }
}

using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Commands.Models
{
    public class EditRolesForUserCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public List<string> Roles { get; set; } = null!;
    }
}

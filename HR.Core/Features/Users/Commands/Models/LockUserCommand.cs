using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Commands.Models
{
    public class LockUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public LockUserCommand(string id)
        {
            Id = id;
        }
    }
}

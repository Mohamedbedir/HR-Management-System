using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Commands.Models
{
    public class UnLockUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public UnLockUserCommand(int id)
        {
            Id = id;
        }
    }
}

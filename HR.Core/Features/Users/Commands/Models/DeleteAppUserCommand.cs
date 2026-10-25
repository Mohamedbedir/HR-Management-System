using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Commands.Models
{
    public class DeleteAppUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteAppUserCommand(int id)
        {
            Id = id;
        }
    }
}
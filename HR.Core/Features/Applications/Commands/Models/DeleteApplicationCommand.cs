using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Applications.Commands.Models
{
    public class DeleteApplicationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteApplicationCommand(int id)
        {
            Id = id;
        }
    }
}

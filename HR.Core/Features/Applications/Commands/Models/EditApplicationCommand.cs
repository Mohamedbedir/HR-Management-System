using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Applications.Commands.Models
{
    public class EditApplicationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
    }
}

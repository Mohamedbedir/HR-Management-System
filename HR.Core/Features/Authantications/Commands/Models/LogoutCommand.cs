using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Authantications.Commands.Models
{
    public class LogoutCommand : IRequest<Response<string>>
    {
        public string RefreshToken { get; set; } = null!;
    }
}

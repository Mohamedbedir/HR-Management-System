using HR.Core.Bases;
using HR.Core.Features.Authantications.Commands.Respnses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Authantications.Commands.Models
{
    public class LoginCommand :IRequest<Response<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

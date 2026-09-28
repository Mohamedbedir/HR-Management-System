using HR.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task<LoginResult> LoginAsync(string email,string password);
    }
}

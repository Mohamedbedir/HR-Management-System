using HR.Core.Features.Users.Queries.Resposes;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Users
{
    public partial class UserProfile
    {
  
        public void GetAllUsersMapping()
        {
            CreateMap<ApplicationUser, GetAllUsersResponse>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Employee == null
                    ? null
                    : s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.Employee == null
                    ? null
                    : s.Employee.Phone));
        }
    }
}

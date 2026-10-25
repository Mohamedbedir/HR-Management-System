using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Users.Queries.Models;
using HR.Core.Features.Users.Queries.Resposes;
using HR.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Queries.Handlers
{
    public class UsersQueryHandler : ResponseHandler,
        IRequestHandler<GetAllUsersQuery, Response<IReadOnlyList<GetAllUsersResponse>>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UsersQueryHandler(IStringLocalizer<SharedResources> localizer,
            UserManager<ApplicationUser> userManager,
            IMapper mapper) : base(localizer)
        {
            Localizer = localizer;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IStringLocalizer<SharedResources> Localizer { get; }

        public async Task<Response<IReadOnlyList<GetAllUsersResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var alluser = userManager.Users.Include(e => e.Employee).AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.searsh))
            {
                var search = request.searsh.Trim().ToLowerInvariant();
                alluser = alluser.Where(u =>
                    (u.Email != null && u.Email.ToLower().Contains(search)) ||
                    (u.PhoneNumber != null && u.Employee.Phone.ToLower().Contains(search)) ||
                    (u.Employee != null && u.Employee.FirstName.ToLower().Contains(search)) ||
                    (u.Employee != null && u.Employee.LastName.ToLower().Contains(search)));
            }
            var users = await alluser.ToListAsync(cancellationToken);
            //GetAllUsersResponse response = new GetAllUsersResponse();
            //response.Roles=userManager.GetRolesAsync(users.FirstOrDefault()).Result.ToList();
            var mappedUser = mapper.Map<IReadOnlyList<GetAllUsersResponse>>(users);
            foreach(var user in mappedUser)
            {
                var dbuser = users.FirstOrDefault(u => u.Id == user.Id);
                if(dbuser != null)
                {
                    user.Roles = (await userManager.GetRolesAsync(dbuser)).ToList();
                }
            }

            return Success(mappedUser);
        }
    }
}

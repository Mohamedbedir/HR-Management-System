using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Departments.Queries.Models;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler: ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery,Response<GetDepartmentByIdResponse>>,
        IRequestHandler<GetDepartmentsQuery, Response<IReadOnlyList<GetDepartmentsResponse>>>
    {
        private readonly IDepartmentService departmentService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public DepartmentQueryHandler(IDepartmentService departmentService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer)
        {
            this.departmentService = departmentService;
            this.mapper = mapper;
            this.localizer = localizer;
        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department= await departmentService.GetDepartmentByIdAsync(request.Id);
            if (department == null)
                return NotFound<GetDepartmentByIdResponse>();
            var dep_Mapped = mapper.Map<GetDepartmentByIdResponse>(department);
            return Success(dep_Mapped);
        }

        public async Task<Response<IReadOnlyList<GetDepartmentsResponse>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var Depars = await departmentService.GetAllDepartmentsAsync();
            var Departs_Mapped = mapper.Map<IReadOnlyList<GetDepartmentsResponse>>(Depars);
            return Success(Departs_Mapped, Meta: new { DataCount = Departs_Mapped.Count() });
        }
    }
}

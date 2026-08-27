using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Departments.Commands.Models;
using HR.Data.Entities;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Departments.Commands.Handlers
{
    public class DepartmentCommandHandler : ResponseHandler,
        IRequestHandler<AddDepartmentCommand, Response<string>>,
        IRequestHandler<EditDepartmentCommand, Response<string>>,
        IRequestHandler<DeleteDepartmentCommand, Response<string>>
    {
        private readonly IDepartmentService departmentService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public DepartmentCommandHandler(IDepartmentService departmentService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer) 
        {
            this.departmentService = departmentService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var studentMapping = mapper.Map<Department>(request);
            var res = await departmentService.CreateDepartmentAsync(studentMapping);
            //if (res == "Exist")
            //    return UnprocessableEntity<string>("Student Name Is Exist");
            if (res == "Success")
                return Created<string>("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var depart = await departmentService.GetDepartmentByIdAsync(request.Id);

            if (depart == null)
                return NotFound<string>();

            var resdelete = await departmentService.DeleteDepartmentAsync(depart);

            if (resdelete != "Success")
                return BadRequest<string>();

            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            var depart = await departmentService.GetDepartmentByIdAsync(request.Id);

            if (depart == null)
                return NotFound<string>();

            depart.Name = request.Name;
            depart.Description=request.Description;
            depart.IsActive = request.IsActive;

            var resdelete = await departmentService.UpdateDepartmentAsync(depart);

            if (resdelete != "Success")
                return BadRequest<string>();

            return Updated<string>("");
        }
    }
}

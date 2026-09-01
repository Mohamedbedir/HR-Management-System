using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Employees.Commands.Models;
using HR.Data.Entities;
using HR.Service.Services;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Employees.Commands.Handlers
{
    public class EmployeeCommandHandler : ResponseHandler,
        IRequestHandler<AddEmployeeCommand, Response<string>>,
        IRequestHandler<EditEmployeeCommand, Response<string>>,
        IRequestHandler<DeleteEmployeeCommand, Response<string>>
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;

        public EmployeeCommandHandler(IStringLocalizer<SharedResources> localizer,
            IEmployeeService employeeService,
            IMapper mapper) : base(localizer)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var EmpMapping = mapper.Map<Employee>(request);
            var res = await employeeService.AddEmployeeAsync(EmpMapping);
            //if (res == "Exist")
            //    return UnprocessableEntity<string>("Student Name Is Exist");
            if (res == "Success")
                return Created<string>("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var Emp = await employeeService.GetEmployeeByIdAsync(request.Id);

            if (Emp == null)
                return NotFound<string>();

            try
            {
                var resdelete = await employeeService.DeleteEmployeeAsync(Emp);

                if (resdelete != "Success")
                    return BadRequest<string>();

                return Deleted<string>();
            }
            catch (Exception ex)
            {
                return Conflict<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(request.Id);

            if (employee == null)
                return NotFound<string>();

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
            employee.Phone = request.Phone;
            employee.Address = request.Address;
            employee.BirthDate = request.BirthDate;
            employee.HireDate = request.HireDate;
            employee.TerminationDate = request.TerminationDate;
            employee.Salary = request.Salary;
            employee.Status = request.Status;
            employee.Gender = request.Gender;
            employee.DepartmentId = request.DepartmentId;
            employee.PositionId = request.PositionId;
            employee.ManagerId = request.ManagerId;

            var resdelete = await employeeService.UpdateEmployeeAsync(employee);

            if (resdelete != "Success")
                return BadRequest<string>();

            return Updated<string>("");
        }
    }
}

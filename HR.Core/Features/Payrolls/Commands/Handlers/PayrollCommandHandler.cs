using HR.Core.Bases;
using HR.Core.Features.Payrolls.Commands.Models;
using HR.Data.Enums;
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

namespace HR.Core.Features.Payrolls.Commands.Handlers
{
     
    public class PayrollCommandHandler
    : ResponseHandler,
      IRequestHandler<CalculatePayrollCommand, Response<string>>,
      IRequestHandler<CancelPayrollCommand, Response<string>>,
      IRequestHandler<ApprovePayrollCommand, Response<string>>,
      IRequestHandler<PayPayrollCommand, Response<string>>
    {
        private readonly IEmployeeService employeeService;
        private readonly IPayrollService payrollService;
        private readonly IPayrollCalculationService payrollCalculationService;

        public PayrollCommandHandler(IStringLocalizer<SharedResources> localizer,
            IEmployeeService employeeService,
       IPayrollService payrollService,
        IPayrollCalculationService payrollCalculationService) : base(localizer)
        {
            this.employeeService = employeeService;
            this.payrollService = payrollService;
            this.payrollCalculationService = payrollCalculationService;
        }

        

        public async Task<Response<string>> Handle( CalculatePayrollCommand request,
            CancellationToken cancellationToken)
        {
            // =========================
            // 1. Check Employee
            // =========================

            var employee =
                await employeeService.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null)
                return NotFound<string>("Employee not found.");

            // =========================
            // 2. Check Employee Status
            // =========================

            if (employee.Status != EmployeeStatus.Active)
                return BadRequest<string>(
                    "Only active employees can have payroll calculated.");

            // =========================
            // 3. Check Duplicate Payroll
            // =========================

            var payrollExists =
                await payrollService.IsPayrollExistForMonthAsync(
                    request.EmployeeId,
                    request.Month,
                    request.Year);

            if (payrollExists)
                return Conflict<string>(
                    "Payroll already exists for this employee and month.");

            // =========================
            // 4. Calculate Payroll
            // =========================

            var payroll =
                await payrollCalculationService.CalculateAsync(
                    employee,
                    request.Month,
                    request.Year,
                    request.Bonus,
                    request.Deduction);

            // =========================
            // 5. Add Payroll
            // =========================

            await payrollService.AddAsync(payroll);

            // =========================
            // 6. Save Changes
            // =========================

            await payrollService.SaveChangesAsync();

            return Success<string>(
                "Payroll calculated successfully.");
        }

        public async Task<Response<string>> Handle(CancelPayrollCommand request, CancellationToken cancellationToken)
        {
            var payroll = await payrollService.GetByIdAsync(request.PayrollId);

            if (payroll == null)
                return NotFound<string>("There is no Payroll Found");

            if (payroll.Status != PayrollStatus.Draft && payroll.Status != PayrollStatus.Generated
                && payroll.Status != PayrollStatus.Calculated)
            {
                return BadRequest<string>(
                    "Only draft , generated or calculated payrolls can be cancelled.");
            }

            payroll.Status = PayrollStatus.Cancelled;

            await payrollService.UpdateAsync(payroll);
            await payrollService.SaveChangesAsync();

            return Success<string>("Payroll cancelled successfully.");
        }

        public async Task<Response<string>> Handle(ApprovePayrollCommand request, CancellationToken cancellationToken)
        {
            var payroll = await payrollService.GetByIdAsync(request.PayrollId);

            if (payroll == null)
                return NotFound<string>("There is no Payroll Found");

            if (payroll.Status != PayrollStatus.Calculated)
                return BadRequest<string>("Only calculated payrolls can be approved.");

            payroll.Status = PayrollStatus.Approved;
            payroll.ApprovedAt = DateTime.UtcNow;

            await payrollService.UpdateAsync(payroll);
            await payrollService.SaveChangesAsync();

            return Success<string>("Payroll approved successfully.");
        }

        public async Task<Response<string>> Handle(PayPayrollCommand request, CancellationToken cancellationToken)
        {
            var payroll = await payrollService.GetByIdAsync(request.PayrollId);

            if (payroll == null)
                return NotFound<string>("There is no Payroll Found");

            if(payroll.Status != PayrollStatus.Approved)
                return BadRequest<string>("Only approved payrolls can be paid.");

            payroll.Status = PayrollStatus.Paid;
            payroll.PaidAt = DateTime.UtcNow;

            await payrollService.UpdateAsync(payroll);  
            await payrollService.SaveChangesAsync();

            return Success<string>("Payroll paid successfully.");
        }
    }
}

using HR.Data.Entities;
using HR.Service.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class PayrollCalculationService : IPayrollCalculationService
    {
        private readonly IEmployeeService employeeService;
        private readonly IPayrollService payrollService;

        public PayrollCalculationService(IEmployeeService employeeService,
            IPayrollService payrollService)
        {
            this.employeeService = employeeService;
            this.payrollService = payrollService;
        }
        public Task<Payroll> CalculateAsync(int employeeId, int month, int year, decimal bonus, decimal deduction)
        {
            throw new NotImplementedException();
        }
    }
}

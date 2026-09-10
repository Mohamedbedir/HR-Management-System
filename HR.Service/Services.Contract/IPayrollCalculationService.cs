using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IPayrollCalculationService
    {
        Task<Payroll> CalculateAsync(Employee employee,int month,int year,decimal bonus,decimal deduction);
    }
}

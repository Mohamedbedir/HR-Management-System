using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Payrolls
{
    public partial class PayrollProfile : Profile
    {
        public PayrollProfile()
        {
            GetPayrollByIdMapping();
            GetPayrollsMapping();
            GetPayrollsForEmployeeMapping();
        }
    }
}

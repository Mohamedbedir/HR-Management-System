using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Histories
{
    public partial class HistoryProfile:Profile
    {
        public HistoryProfile()
        {
            GetSalaryHistoryForEmployeeMapping();
            GetHistoryForEmployeeMapping();
        }
    }
}

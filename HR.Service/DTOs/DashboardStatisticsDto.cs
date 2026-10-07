using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.DTOs
{
    public class DashboardStatisticsDto
    {
        public int TotalEmployees { get; set; }

        public int PresentToday { get; set; }

        public int PendingLeaves { get; set; }

        public decimal MonthlyPayroll { get; set; }
    }
}

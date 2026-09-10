using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Enums
{
    public enum PayrollStatus
    {
        Draft = 1,
        Generated = 2,
        Calculated= 3,
        Approved = 4,
        Paid = 5,
        Cancelled = 6
    }
}

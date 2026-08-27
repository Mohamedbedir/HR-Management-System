using HR.Data.Entities.Common;
using HR.Data.Enums;

namespace HR.Data.Entities
{
    public class PayrollItem : BaseEntity
    {
        public string Name { get; set; } = null!;

        public PayrollItemType Type { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        // Navigation
        public int PayrollId { get; set; }
        public Payroll Payroll { get; set; } = null!;
    }
}
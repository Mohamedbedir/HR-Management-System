using HR.Data.Entities.Common;
using HR.Data.Enums;

namespace HR.Data.Entities
{
    public class PayrollItem : BaseEntity
    {
        public int PayrollId { get; set; }

        public string Name { get; set; } = null!;

        public PayrollItemType Type { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        // Navigation
        public Payroll Payroll { get; set; } = null!;
    }
}
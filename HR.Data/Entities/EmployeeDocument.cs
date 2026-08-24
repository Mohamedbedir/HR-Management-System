using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class EmployeeDocument
    {
        public long Id { get; set; }

        public string DocumentType { get; set; } 

        public string FileName { get; set; } = null!;

        public string FilePath { get; set; } = null!;

        public DateTime UploadedAt { get; set; }

        public DateTime? ExpiryDate { get; set; }

        // Navigation
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}

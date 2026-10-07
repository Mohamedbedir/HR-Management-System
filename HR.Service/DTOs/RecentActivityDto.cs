using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.DTOs
{
    public class RecentActivityDto
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Time { get; set; }

        public string Icon { get; set; } = null!;
    }
}

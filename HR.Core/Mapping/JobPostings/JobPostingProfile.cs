using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.JobPostings
{
    public partial class JobPostingProfile:Profile

    {
        public JobPostingProfile()
        {
            GetJobPostingsMapping();
            GetJobPostingByIdMapping();
        }
    }
}

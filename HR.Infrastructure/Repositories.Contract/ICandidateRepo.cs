using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories.Contract
{
    public interface ICandidateRepo: IGenericRepos<HR.Data.Entities.Recruitment.Candidate>
    {
    }
}

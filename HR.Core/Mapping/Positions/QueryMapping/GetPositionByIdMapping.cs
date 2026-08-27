using HR.Core.Features.Positions.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Positions
{
    public partial class PositionProfile
    {
        public void GetPositionByIdMapping() 
        {
            CreateMap<Position, GetPositionByIdResponse>();
        }
    }
}

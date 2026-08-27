using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Positions
{
    public partial class PositionProfile:Profile
    {
        public PositionProfile() 
        {
            GetPositionByIdMapping();
            GetPositionsMapping();

            AddPositionMapping();
            EditPositionMapping();
        }
    }
}

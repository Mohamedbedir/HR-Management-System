using HR.Core.Features.Positions.Commands.Models;
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
        public void AddPositionMapping()
        {
            CreateMap<AddPositionCommand, Position>();
        }
    }
}

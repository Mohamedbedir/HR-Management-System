using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IPositionService
    {
        Task<Position?> GetPositionByIdAsync(int id);

        Task<IReadOnlyList<Position>> GetAllPositionsAsync();

        Task<string> CreatePositionAsync(Position position);

        Task<string> UpdatePositionAsync(Position position);

        Task<string> DeletePositionAsync(Position position);
        Task<bool> IsPositionExist(string title);
        Task<bool> IsPositionExistExcludeSelf(string title, int id);
    }
}

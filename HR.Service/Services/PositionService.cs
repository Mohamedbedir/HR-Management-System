using HR.Data.Entities;
using HR.Infrastructure.Repositories;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepo positionRepo;

        public PositionService(IPositionRepo positionRepo)
        {
            this.positionRepo = positionRepo;
        }
        
        public async Task<IReadOnlyList<Position>> GetAllPositionsAsync()
        {
            var Positions= await positionRepo.GetAllAsync();
            return Positions;
        }

        public async Task<Position?> GetPositionByIdAsync(int id)
        {
            var Position= await positionRepo.GetByIdAsync(id);
            return Position;
        }

        public async Task<string> CreatePositionAsync(Position Position)
        {
            await positionRepo.AddAsync(Position);
            await positionRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> DeletePositionAsync(Position Position)
        {
           try
           { 
                positionRepo.DeleteAsync(Position);
                await positionRepo.SaveChangesAsync();
                return "Success";
           }
           catch (DbUpdateException ex)
           {
                if (ex.InnerException?.Message.Contains(
                        "REFERENCE constraint",
                        StringComparison.OrdinalIgnoreCase) == true)
                {
                    throw new Exception(
                        "Cannot delete this Postion because it is assigned to other records.");
                }

                throw;
            }
        }

        public async Task<string> UpdatePositionAsync(Position Position)
        {
            positionRepo.UpdateAsync(Position);
            await positionRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> IsPositionExist(string title)
        {
             return await positionRepo.GetTableNoTracking().AnyAsync(d => d.Title == title);
        }

        public async Task<bool> IsPositionExistExcludeSelf(string title, int id)
        {
            var depart = await positionRepo.GetTableNoTracking()
                .Where(d => d.Title == title && !d.Id.Equals(id)).FirstOrDefaultAsync();
            if(depart== null)
                return false;
            return true;
        }

        public async Task<bool> IsPositionExistById(int id)
        {
            return await positionRepo.GetTableNoTracking().AnyAsync(d => d.Id == id);
        }
    }
}

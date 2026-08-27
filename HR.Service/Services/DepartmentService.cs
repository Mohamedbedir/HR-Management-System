using HR.Data.Entities;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo departmentRepo;

        public DepartmentService(IDepartmentRepo departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }
        
        public async Task<IReadOnlyList<Department>> GetAllDepartmentsAsync()
        {
            var departments= await departmentRepo.GetAllAsync();
            return departments;
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            var department= await departmentRepo.GetByIdAsync(id);
            return department;
        }

        public async Task<string> CreateDepartmentAsync(Department department)
        {
            await departmentRepo.AddAsync(department);
            await departmentRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> DeleteDepartmentAsync(Department department)
        {
            departmentRepo.DeleteAsync(department);
            await departmentRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> UpdateDepartmentAsync(Department department)
        {
            departmentRepo.UpdateAsync(department);
            await departmentRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> IsDepartmentExist(string name)
        {
             return await departmentRepo.GetTableNoTracking().AnyAsync(d => d.Name == name);
        }

        public async Task<bool> IsDepartmentExistExcludeSelf(string name, int id)
        {
            var depart = await departmentRepo.GetTableNoTracking()
                .Where(d => d.Name == name && !d.Id.Equals(id)).FirstOrDefaultAsync();
            if(depart== null)
                return false;
            return true;
        }
    }
}

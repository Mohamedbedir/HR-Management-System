using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IDepartmentService
    {
        Task<Department?> GetDepartmentByIdAsync(int id);

        Task<IReadOnlyList<Department>> GetAllDepartmentsAsync();

        Task<string> CreateDepartmentAsync(Department department);

        Task<string> UpdateDepartmentAsync(Department department);

        Task<string> DeleteDepartmentAsync(Department department);
        Task<bool> IsDepartmentExist(string name);
        Task<bool> IsStudentExistExcludeSelf(string name,int id);
    }
}

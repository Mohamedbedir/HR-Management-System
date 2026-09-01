using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IEmployeeService
    {
        Task<bool> IsEmployeeExist(string fname,string lname);
        Task<bool> IsEmployeeExistById(int id);
        Task<bool> IsNameExistExcludeSelf(string fname, string lname,int id);

        Task<bool> IsEmailExist(string email);
        Task<bool> IsEmailExistExcludeSelf(string email,int id);

        Task<bool> IsPhoneExist(string phone);
        Task<bool> IsPhoneExistExcludeSelf(string phone,int id);

        Task<Employee?> GetEmployeeByIdAsync(int id);

        Task<IReadOnlyList<Employee>> GetEmployeesAsync();

        Task<string> AddEmployeeAsync(Employee employee);

        Task<string> UpdateEmployeeAsync(Employee employee);

        Task<string> DeleteEmployeeAsync(Employee employee);
    }
}

using HR.Data.Entities;
using HR.Data.Enums;
using HR.Infrastructure.Repositories;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HR.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo employeeRepo;
        private readonly IEmploymentHistoryRepo employmentHistoryRepo;
        private readonly ISalaryHistoryRepo salaryHistoryRepo;

        public EmployeeService(IEmployeeRepo employeeRepo ,
            IEmploymentHistoryRepo employmentHistoryRepo,
            ISalaryHistoryRepo salaryHistoryRepo)
        {
            this.employeeRepo = employeeRepo;
            this.employmentHistoryRepo = employmentHistoryRepo;
            this.salaryHistoryRepo = salaryHistoryRepo;
        }

        public async Task<bool> IsEmployeeUnderManagerAsync(int employeeId,int managerId)
        {
            var employee = await employeeRepo.GetByIdAsync(employeeId);

            if (employee is null)
                return false;

            return employee.ManagerId == managerId;
        }
        public async Task<string> AddEmployeeAsync(Employee employee)
        {
            var transaction = employeeRepo.BeginTransactionAsync();

            try
            {
                await employeeRepo.AddAsync(employee);

                var employmentHistory = new EmploymentHistory
                {
                    Employee = employee,
                    DepartmentId = employee.DepartmentId!.Value,
                    PositionId = employee.PositionId!.Value,
                    StartDate = employee.HireDate,
                    Reason = "Initial Hiring"
                };

                await employmentHistoryRepo.AddAsync(employmentHistory);

                var salaryHistory = new SalaryHistory
                {
                    Employee = employee,
                    Salary = employee.Salary,
                    StartDate = employee.HireDate,
                    Reason = "Initial Salary"
                };

                await salaryHistoryRepo.AddAsync(salaryHistory);

                await employeeRepo.SaveChangesAsync();

                await employeeRepo.CommitAsync();

                return "Success";
            }
            catch
            {
                await employeeRepo.RollBackAsync();
                throw;
            }
        }
       

        public async Task<string> DeleteEmployeeAsync(Employee employee)
        {
            // Check if employee is manager of other employees
            var isUsedAsManager = await employeeRepo.GetTableNoTracking().AnyAsync(e => e.ManagerId == employee.Id);

            if (isUsedAsManager)
            {
                throw new Exception("Cannot delete this employee because they are a manager of other employees.");
            }

            // Check dependent data that has Restrict delete behavior in the model
            var hasEmploymentHistory = await employmentHistoryRepo.GetTableNoTracking().AnyAsync(h => h.EmployeeId == employee.Id);
            if (hasEmploymentHistory)
            {
                throw new Exception("Cannot delete this employee because there are employment history records linked to them. Delete or detach those records first.");
            }

            var hasSalaryHistory = await salaryHistoryRepo.GetTableNoTracking().AnyAsync(h => h.EmployeeId == employee.Id);
            if (hasSalaryHistory)
            {
                throw new Exception("Cannot delete this employee because there are salary history records linked to them. Delete or detach those records first.");
            }

            // No blocking relations detected - perform delete
            employeeRepo.DeleteAsync(employee);
            await employeeRepo.SaveChangesAsync();
            return "Success";

        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            var emp=await employeeRepo.GetTableAsTracking().Where(e=>e.Id==id)
                .Include(d=>d.Department)
                .Include(p=>p.Position)
                .Include(m=>m.Manager)
                .Include(s=>s.Subordinates)
                .FirstOrDefaultAsync();
            return emp;
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeesAsync()
        {
            var emps = await employeeRepo.GetAllAsync();
            return emps;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await employeeRepo.GetTableNoTracking().AnyAsync(e=>e.Email== email);
        }

        public async Task<bool> IsEmailExistExcludeSelf(string email, int id)
        {
            var empExist = await employeeRepo.GetTableNoTracking()
                .Where(e => e.Email == email && !e.Id.Equals(id)).FirstOrDefaultAsync();
            if(empExist == null) 
                return false;
            else return true;
        }

        public async Task<bool> IsEmployeeExist(string fname, string lname)
        {
            var empExist = await employeeRepo.GetTableNoTracking()
                .Where(e => e.FirstName == fname && e.LastName==lname).FirstOrDefaultAsync();
            if (empExist == null)
                return false;
            else return true;
        }

        public async Task<bool> IsEmployeeExistById(int id)
        {
            return await employeeRepo.GetTableNoTracking().AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsNameExistExcludeSelf(string fname, string lname, int id)
        {
            var empExist = await employeeRepo.GetTableNoTracking()
                .Where(e => e.FirstName == fname && e.LastName == lname && !e.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (empExist == null)
                return false;
            else return true;
        }

        public async Task<bool> IsPhoneExist(string phone)
        {
            return await employeeRepo.GetTableNoTracking().AnyAsync(e => e.Phone == phone);
        }

        public async Task<bool> IsPhoneExistExcludeSelf(string phone, int id)
        {
            var empExist = await employeeRepo.GetTableNoTracking()
                .Where(e => e.Phone == phone && !e.Id.Equals(id)).FirstOrDefaultAsync();
            if (empExist == null)
                return false;
            else return true;
        }

        public async Task<string> UpdateEmployeeAsync(Employee employee)
        {
            var oldEmployee = await employeeRepo
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Id == employee.Id);

            if (oldEmployee == null)
                throw new Exception("Employee not found.");

            var transaction = await employeeRepo.BeginTransactionAsync();

            try
            {
                // 1. Salary Changed

                if (oldEmployee.Salary != employee.Salary)
                {
                    var currentHistory = await salaryHistoryRepo
                        .GetTableAsTracking()
                        .FirstOrDefaultAsync(x =>
                            x.EmployeeId == employee.Id &&
                            x.EndDate == null);

                    if (currentHistory != null)
                    {
                        currentHistory.EndDate = DateOnly.FromDateTime(DateTime.Now);

                        salaryHistoryRepo.UpdateAsync(currentHistory);
                    }

                    var salaryHistory = new SalaryHistory
                    {
                        EmployeeId = employee.Id,
                        Salary = employee.Salary,
                        StartDate = DateOnly.FromDateTime(DateTime.Now),
                        Reason = "Salary updated"
                    };

                    await salaryHistoryRepo.AddAsync(salaryHistory);
                }

                // 2. Department / Position Changed

                if (oldEmployee.DepartmentId != employee.DepartmentId ||
                    oldEmployee.PositionId != employee.PositionId)
                {
                    var currentHistory = await employmentHistoryRepo
                        .GetTableAsTracking()
                        .FirstOrDefaultAsync(x =>
                            x.EmployeeId == employee.Id &&
                            x.EndDate == null);

                    if (currentHistory != null)
                    {
                        currentHistory.EndDate = DateOnly.FromDateTime(DateTime.UtcNow);

                        employmentHistoryRepo.UpdateAsync(currentHistory);
                    }

                    if (employee.DepartmentId.HasValue &&
                        employee.PositionId.HasValue)
                    {
                        var employmentHistory = new EmploymentHistory
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentId.Value,
                            PositionId = employee.PositionId.Value,
                            StartDate = DateOnly.FromDateTime(DateTime.Now),
                            Reason = "Department or Position changed"
                        };

                        await employmentHistoryRepo.AddAsync(employmentHistory);
                    }
                }

                // 3. Update Employee

                employeeRepo.UpdateAsync(employee);

                // 4. Save All Changes
                
                await employeeRepo.SaveChangesAsync();

                // 5. Commit

                await employeeRepo.CommitAsync();

                return "Success";
            }
            catch
            {
                // Rollback

                await employeeRepo.RollBackAsync();

                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }

        }

        public async Task<IQueryable<Employee>> FilterEmployeePaginatedQueryable
            (EmployeeOrderEnum orderby, EmployeeStatus? FilterByStatus, string? search)
        {
            var Querable = employeeRepo.GetTableNoTracking()
                .Include(d => d.Department)
                .Include(d => d.Manager)
                .Include(d => d.Position)
                .AsQueryable();
            if (search != null)
            {
                Querable = Querable.Where(s => s.EmployeeNumber.Contains(search) 
                ||s.FirstName.Contains(search) || s.Address.Contains(search));

            }
            if (FilterByStatus != null)
            {
                Querable = Querable.Where(s => s.Status == FilterByStatus);
            }

            switch (orderby)
            {
                
                case EmployeeOrderEnum.FirstName:
                    Querable = Querable.OrderBy(s => s.FirstName);
                    break;
                case EmployeeOrderEnum.LastName:
                    Querable = Querable.OrderBy(s => s.LastName);
                    break;
                case EmployeeOrderEnum.DepartmentName:
                    Querable = Querable.OrderBy(s => s.Department.Name);
                    break;
                case EmployeeOrderEnum.HireDate:
                    Querable = Querable.OrderBy(s => s.HireDate);
                    break;
                default:
                    Querable = Querable.OrderBy(s => s.Id);
                    break;
            }
            return Querable;
        }
    }
}

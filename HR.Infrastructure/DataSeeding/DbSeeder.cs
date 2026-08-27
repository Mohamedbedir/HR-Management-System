using HR.Data.Entities;
using HR.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.DataSeeding
{
    public static class DbSeeder
    {
        //public static void Seed(HRDbContext context)
        //{
        //    SeedDepartments(context);
        //    SeedPositions(context);
        //    SeedLeaveTypes(context);

        //    context.SaveChanges();
        //}

        public async static Task SeedDepartments(HRAppDbContext context)
        {
            if (!context.Departments.Any())
            {

                var departments = new List<Department>
                {
                    new Department
                    {
                        //Id = 1,
                        Name = "IT",
                        Description="This is Information Technology Department",
                        IsActive = true

                    },
                    new Department
                    {
                        //Id = 2,
                        Name = "HR",
                        Description="This is Heumen resource Department",
                        IsActive = true

                    },
                    new Department
                    {
                        //Id = 3,
                        Name = "Finance",
                        Description="This is Finance Department",
                        IsActive = true
                    },
                    new Department
                    {
                        //Id = 4,
                        Name = "Sales",
                        Description="This is Sales Department",
                        IsActive = true
                    }
                };

                context.Departments.AddRange(departments);
                await context.SaveChangesAsync();
            }

        }

        public async static Task SeedPositions(HRAppDbContext context)
        {
            if (!context.Positions.Any())
            {

                var positions = new List<Position>
                {
                    new Position
                    {
                        //Id = 1,
                        Title = "Backend Developer",
                        Description = "This is Backend Developer Position",
                        MinSalary=15000,
                        MaxSalary=20000,
                        IsActive=true

                    },
                    new Position
                    {
                        //Id = 2,
                        Title = "Frontend Developer",
                        Description = "This is Frontend Developer Position",
                        MinSalary=15000,
                        MaxSalary=20000,
                        IsActive=true
                    },
                    new Position
                    {
                        //Id = 3,
                        Title = "HR Specialist",
                        Description = "This is HR Specialist Position",
                        MinSalary=10000,
                        MaxSalary=15000,
                        IsActive=true
                    },
                    new Position
                    {
                        //Id = 4,
                        Title = "Accountant",
                        Description = "This is Accountant Position",
                        MinSalary=14000,
                        MaxSalary=17000,
                        IsActive=true
                    },
                    new Position
                    {
                        //Id = 5,
                        Title = "Sales Executive",
                        Description = "This is Sales Executive Position",
                        MinSalary=10000,
                        MaxSalary=15000,
                        IsActive=true
                    }
                };

                context.Positions.AddRange(positions);
                await context.SaveChangesAsync();
            }

            
        }

        public async static Task SeedLeaveTypes(HRAppDbContext context)
        {
            if (!context.LeaveTypes.Any())
            {

                var leaveTypes = new List<LeaveType>
                {
                    new LeaveType
                    {
                        //Id = 1,
                        Name = "Annual",
                        MaxDaysPerYear = 21,
                        IsPaid = true,
                        IsActive = true
                    },
                    new LeaveType
                    {
                        //Id = 2,
                        Name = "Sick",
                        MaxDaysPerYear = 14,
                        IsPaid = true,
                        IsActive = true
                    },
                    new LeaveType
                    {
                        //Id = 3,
                        Name = "Emergency",
                        MaxDaysPerYear = 7,
                        IsPaid = true,
                        IsActive = true
                    },
                    new LeaveType
                    {
                        //Id = 4,
                        Name = "Unpaid",
                        MaxDaysPerYear = 30,
                        IsPaid = false,
                        IsActive = true
                    }
                };

                context.LeaveTypes.AddRange(leaveTypes);
                await context.SaveChangesAsync();
            }

                    }
    }
}

using HR.Data.Entities;
using HR.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Configurations
{
    internal class EmployeeConfigurations: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // المفتاح الأساسي (مورث من BaseEntity)
            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmployeeNumber)
                    .HasMaxLength(20)
                    .HasComputedColumnSql(
                    "'EMP-'+RIGHT('000000'+CAST([Id] AS varchar(6)),6)", stored: true);
            builder.HasIndex(e => e.EmployeeNumber)
            .IsUnique();
            // يمكنك تغيير الطول حسب حاجتك

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(e => e.Email)
             .IsUnique();

            builder.Property(e => e.Phone)
                .HasMaxLength(20);

            builder.Property(e => e.Address)
                .HasMaxLength(250);

            builder.Property(e => e.Salary)
                .HasColumnType("decimal(18,2)"); // تحديد دقة الرقم العشري

            builder.Property(e => e.Status)
                .IsRequired(false) 
                .HasConversion(
                    statusName => string.IsNullOrEmpty(statusName)
                        ? (int?)null
                        : (int)Enum.Parse<EmployeeStatus>(statusName),

                    statusValue => statusValue == null
                        ? null
                        : ((EmployeeStatus)statusValue).ToString()
                )
                .HasDefaultValue(EmployeeStatus.Active.ToString());

            builder.Property(e => e.Gender)
               .IsRequired(false)
               .HasConversion(
                   gendername => string.IsNullOrEmpty(gendername)
                       ? (int?)null
                       : (int)Enum.Parse<Gender>(gendername),

                   gendervalue => gendervalue == null
                       ? null
                       : ((Gender)gendervalue).ToString()
               );

            builder.Property(e => e.HireDate)
               .IsRequired();

            // إعدادات واجهة IAuditable
            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .IsRequired();

            // --- العلاقات (Relations) ---

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees) 
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(e => e.Position)
                .WithMany(p => p.Employees) 
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Manager)
                .WithMany(e => e.Subordinates)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); 

            
        }

       
    }
}

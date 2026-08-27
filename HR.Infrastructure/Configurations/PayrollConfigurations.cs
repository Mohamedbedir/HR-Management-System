
using HR.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Configurations
{
    internal class PayrollConfigurations : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Month
            builder.Property(x => x.Month)
                .IsRequired();

            // Year
            builder.Property(x => x.Year)
                .IsRequired();

            // BasicSalary
            builder.Property(x => x.BasicSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // GrossSalary
            builder.Property(x => x.GrossSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // TotalDeductions
            builder.Property(x => x.TotalDeductions)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // NetSalary
            builder.Property(x => x.NetSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Status
            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            // GeneratedAt
            builder.Property(x => x.GeneratedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Relationship
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // One payroll per employee per month/year
            builder.HasIndex(x => new
            {
                x.EmployeeId,
                x.Month,
                x.Year
            })
            .IsUnique();
        }
    }
}

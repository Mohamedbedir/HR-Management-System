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

    public class SalaryHistoryConfiguration
        : IEntityTypeConfiguration<SalaryHistory>
    {
        public void Configure(EntityTypeBuilder<SalaryHistory> builder)
        {


            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            // Salary
            builder.Property(x => x.Salary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Dates
            builder.Property(x => x.StartDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnType("datetime2");

            // Reason
            builder.Property(x => x.Reason)
                .HasMaxLength(500);

            // Relationship
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index
            builder.HasIndex(x => x.EmployeeId);
        }
    }
}

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
    public class EmploymentHistoryConfiguration
    : IEntityTypeConfiguration<EmploymentHistory>
    {
        public void Configure(EntityTypeBuilder<EmploymentHistory> builder)
        {
            

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            // Relationships

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Position)
                .WithMany()
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Properties

            builder.Property(x => x.StartDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnType("datetime2");

            builder.Property(x => x.Reason)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(x => x.EmployeeId);
            builder.HasIndex(x => x.DepartmentId);
            builder.HasIndex(x => x.PositionId);
        }
    }
}

using HR.Data.Entities;
using HR.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Configurations
{
    internal class AttendanceConfigurations : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(x => x.Id);  

         
            // Date
            builder.Property(x => x.Date)
                .HasColumnType("date")
                .IsRequired();

            // CheckIn
            builder.Property(x => x.CheckIn)
                .HasColumnType("time");

            // CheckOut
            builder.Property(x => x.CheckOut)
                .HasColumnType("time");

            // Status
            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            // LateMinutes
            builder.Property(x => x.LateMinutes)
                .IsRequired();

            // OvertimeMinutes
            builder.Property(x => x.OvertimeMinutes)
                .IsRequired();

            // Notes
            builder.Property(x => x.Notes)
                .HasMaxLength(500);

            // Relationship
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Important:
            // One employee should have only one attendance record per day.
            builder.HasIndex(x => new
            {
                x.EmployeeId,
                x.Date
            })
            .IsUnique();
        }
    }
}

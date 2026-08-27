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
    internal class LeaveRequestConfigurations : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {

            // Primary Key
            builder.HasKey(x => x.Id);

            // StartDate
            builder.Property(x => x.StartDate)
                .HasColumnType("date")
                .IsRequired();

            // EndDate
            builder.Property(x => x.EndDate)
                .HasColumnType("date")
                .IsRequired();

            // Reason
            builder.Property(x => x.Reason)
                .HasMaxLength(1000);

            // Status
            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            // ApprovedAt
            builder.Property(x => x.ApprovedAt)
                .HasColumnType("datetime2");

            // CreatedAt
            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Employee -> LeaveRequests
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // LeaveType -> LeaveRequests
            builder.HasOne(x => x.LeaveType)
                .WithMany(x => x.LeaveRequests)
                .HasForeignKey(x => x.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee -> ApprovedBy
            builder.HasOne(x => x.ApprovedBy)
                .WithMany()
                .HasForeignKey(x => x.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}

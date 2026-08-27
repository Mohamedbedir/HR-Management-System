using HR.Data.Entities.Recruitment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Configurations
{
    internal class JobPostingConfigurations : IEntityTypeConfiguration<JobPosting>
    {
        public void Configure(EntityTypeBuilder<JobPosting> builder)
        {

            // Primary Key
            builder.HasKey(x => x.Id);

            // Title
            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            // Description
            builder.Property(x => x.Description)
                .HasMaxLength(4000);

            // Salary
            builder.Property(x => x.MinSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.MaxSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Status
            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            // PostedAt
            builder.Property(x => x.PostedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // ClosingDate
            builder.Property(x => x.ClosingDate)
                .HasColumnType("datetime2");

            // Department Relationship
            builder.HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Position Relationship
            builder.HasOne(x => x.Position)
                .WithMany()
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
           
        }
    }
}

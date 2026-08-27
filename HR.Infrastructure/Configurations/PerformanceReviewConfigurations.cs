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
    internal class PerformanceReviewConfigurations : IEntityTypeConfiguration<PerformanceReview>
    {
        public void Configure(EntityTypeBuilder<PerformanceReview> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // ReviewDate
            builder.Property(x => x.ReviewDate)
                .HasColumnType("datetime2")
                .IsRequired();

            // Score
            builder.Property(x => x.Score)
                .HasColumnType("decimal(3,2)")
                .IsRequired();

            // Comments
            builder.Property(x => x.Comments)
                .HasMaxLength(2000);

            // CreatedAt
            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Employee being reviewed
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee who reviewed
            builder.HasOne(x => x.Reviewer)
                .WithMany()
                .HasForeignKey(x => x.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.EmployeeId,
                x.ReviewDate
            });
        }
    }
}

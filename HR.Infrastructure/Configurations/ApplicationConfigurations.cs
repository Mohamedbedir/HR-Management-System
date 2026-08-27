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
    internal class ApplicationConfigurations : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // AppliedAt
            builder.Property(x => x.AppliedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Status
            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            // Notes
            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            // Candidate Relationship
            builder.HasOne(x => x.Candidate)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => x.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            // JobPosting Relationship
            builder.HasOne(x => x.JobPosting)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => x.JobPostingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent same candidate from applying
            // to the same job more than once.
            builder.HasIndex(x => new
            {
                x.CandidateId,
                x.JobPostingId
            })
            .IsUnique();
        }
    }
}

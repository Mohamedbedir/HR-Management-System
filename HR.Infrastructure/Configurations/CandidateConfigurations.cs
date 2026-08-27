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
    internal class CandidateConfigurations : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
 
            // Primary Key
            builder.HasKey(x => x.Id);

            // FirstName
            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            // LastName
            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            // Email
            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            // Phone
            builder.Property(x => x.Phone)
                .HasMaxLength(20);

            // CVPath
            builder.Property(x => x.CVPath)
                .HasMaxLength(500);

            // CreatedAt
            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Index
            builder.HasIndex(x => x.Email);
        }
    }
}

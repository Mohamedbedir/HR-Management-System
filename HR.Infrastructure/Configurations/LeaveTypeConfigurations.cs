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
    internal class LeaveTypeConfigurations: IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {

            // Primary Key
            builder.HasKey(x => x.Id);

            // Name
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Description
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            // MaxDaysPerYear
            builder.Property(x => x.MaxDaysPerYear)
                .IsRequired();

            // IsPaid
            builder.Property(x => x.IsPaid)
                .IsRequired();

            // IsActive
            builder.Property(x => x.IsActive)
                .IsRequired();

            
        }
    }
}

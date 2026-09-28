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
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // One Employee can have only one ApplicationUser
            builder.HasIndex(x => x.EmployeeId)
                .IsUnique()
                .HasFilter("[EmployeeId] IS NOT NULL");

            // ApplicationUser -> Employee
            builder.HasOne(x => x.Employee)
                .WithOne()
                .HasForeignKey<ApplicationUser>(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

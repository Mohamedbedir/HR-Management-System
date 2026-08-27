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
    internal class PayrollItemConfigurations : IEntityTypeConfiguration<PayrollItem>
    {
        public void Configure(EntityTypeBuilder<PayrollItem> builder)
        {

            // Primary Key
            builder.HasKey(x => x.Id);

            // Name
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Type
            builder.Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            // Amount
            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Description
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            // Relationship
            builder.HasOne(x => x.Payroll)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

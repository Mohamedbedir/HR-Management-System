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

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion(
                    statusName => (int)Enum.Parse<AttendanceStatus>(statusName.),

                    // 2. عند القراءة إذا كان الرقم في الداتا بيز Null يرجع null، وإلا يحوله إلى نص اسم الـ Enum
                    statusValue =>(AttendanceStatus)statusValue).ToString()
                ) ;
        }
    }
}

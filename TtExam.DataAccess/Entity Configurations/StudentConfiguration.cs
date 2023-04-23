using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Domain;

namespace TtExam.DataAccess.Entity_Configurations
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.ToTable(nameof(Student));
            builder.HasKey(std => std.Id);
            builder.Property(std => std.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(std => std.LastName).IsRequired().HasMaxLength(50);
            builder.Property(std => std.StudentNumber).IsRequired().HasMaxLength(50);
            builder.Property(std => std.PhoneNumber).IsRequired().HasMaxLength(50);
            builder.Property(std => std.DateOfBirth).HasColumnType("date");
            builder.Property(std => std.CreatedDate).HasDefaultValueSql("getdate()");
            builder.HasIndex(std => std.IdentityNumber).IsUnique();

            
        }
             
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Domain;

namespace TtExam.DataAccess.Entity_Configurations
{
    internal class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {

            builder.ToTable(nameof(Exam));
            builder.HasKey(exam => exam.Id);
            builder.Property(exam => exam.Name).IsRequired().HasMaxLength(50);
            builder.Property(exam => exam.Date).HasColumnType("date");
            builder.Property(exam => exam.Status).HasConversion(v => (byte)v,
                                                                v => (ExamStatus)Enum.Parse(typeof(ExamStatus), v.ToString()));

        }
    }
}

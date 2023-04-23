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
    internal class StudentExamConfiguration : IEntityTypeConfiguration<StudentExam>
    {
        public void Configure(EntityTypeBuilder<StudentExam> builder)
        {

            builder.ToTable(nameof(StudentExam));
            builder.HasKey(se => new { se.StudentId, se.ExamId});
            builder.Property(se => se.Point).HasColumnType("decimal(6,2)");
            builder.HasOne(se => se.Student)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(s => s.StudentId).OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(se => se.Exam)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(s => s.ExamId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

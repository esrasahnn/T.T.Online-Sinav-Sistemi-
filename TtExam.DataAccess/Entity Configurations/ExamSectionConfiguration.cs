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
    internal class ExamSectionConfiguration : IEntityTypeConfiguration<ExamSection>
    {
        public void Configure(EntityTypeBuilder<ExamSection> builder)
        {

            builder.ToTable(nameof(ExamSection));
            builder.HasKey(exam => exam.Id);

            builder.HasOne(es => es.Exam)
                .WithMany(ex => ex.ExamSections)
                .HasForeignKey(es => es.ExamId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(es => es.Lesson)
                .WithMany(l => l.ExamSections)
                .HasForeignKey(es => es.LessonId).OnDelete(DeleteBehavior.NoAction);



        }
    }
}

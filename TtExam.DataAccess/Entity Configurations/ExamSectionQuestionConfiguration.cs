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
    public class ExamSectionQuestionConfiguration : IEntityTypeConfiguration<ExamSectionQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamSectionQuestion> builder)
        {

            builder.ToTable(nameof(ExamSectionQuestion));
            builder.HasKey(se => new { se.ExamSectionId, se.QuestionId });

            builder.HasOne(se => se.ExamSection)
                .WithMany(s => s.ExamSectionQuestions)
                .HasForeignKey(s => s.ExamSectionId).OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(se => se.Question)
                .WithMany(s => s.ExamSectionQuestions)
                .HasForeignKey(s => s.QuestionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

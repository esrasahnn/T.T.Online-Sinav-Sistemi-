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
    internal class QuestionConfiguration: IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {

            builder.ToTable(nameof(Question));
            builder.HasKey(que => que.Id);
            builder.Property(que => que.Sentence).IsRequired().HasMaxLength(100);

            builder.HasOne(q=> q.Lesson).WithMany(l=> l.Questions).HasForeignKey(q=>q.LessonId).OnDelete(DeleteBehavior.Cascade);    
        }

    }
}

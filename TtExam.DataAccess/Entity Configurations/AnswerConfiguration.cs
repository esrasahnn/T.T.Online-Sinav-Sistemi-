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
    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {

            builder.ToTable(nameof(Answer));
            builder.HasKey(answer => answer.Id);
            builder.Property(answer=> answer.Text).IsRequired().HasMaxLength(50);

            builder.HasOne(a => a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}

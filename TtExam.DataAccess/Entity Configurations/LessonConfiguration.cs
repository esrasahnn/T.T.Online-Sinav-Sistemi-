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
    internal class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {

            builder.ToTable(nameof(Lesson));
            builder.HasKey(les => les.Id);
            builder.Property(les => les.Name).IsRequired().HasMaxLength(50);
            builder.Property(les => les.Description).HasMaxLength(100);


        }
    }
}

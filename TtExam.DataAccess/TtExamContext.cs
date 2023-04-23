using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.DataAccess.Entity_Configurations;
using TtExam.Domain;

namespace TtExam.DataAccess
{
    public class TtExamContext : DbContext
    {

        private const string ConnectionString = "Server=.; Database=TtExam; Integrated Security=true";
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExamSection> ExamSections { get; set; }
        public DbSet<ExamSectionQuestion> ExamSectionQestions { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}

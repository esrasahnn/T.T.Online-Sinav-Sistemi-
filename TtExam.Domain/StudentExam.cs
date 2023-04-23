using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TtExam.Domain
{
    public class StudentExam
    {
        public int StudentId { get; set; }  
        public Student Student { get; set; }

        public int ExamId { get; set; } 
        public Exam Exam { get; set; }

        public double Point { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TtExam
{
    public class QuestionSummary
    {
       
        public int Id { get; set; }

        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string Sentence { get; set; }
        public IEnumerable<ExamSectionDto> Exams { get; set; }
    }
}

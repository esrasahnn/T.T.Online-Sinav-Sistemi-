using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TtExam
{
    public class ExamSectionSummary
    {
        public int Id { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }    
        public int ExamId { get; set; }

      
    }
}

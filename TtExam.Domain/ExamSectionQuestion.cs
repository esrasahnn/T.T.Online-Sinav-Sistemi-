using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TtExam.Domain
{
     public class ExamSectionQuestion
    {
        public int ExamSectionId { get; set; }  
        public ExamSection ExamSection { get; set; }    

        public int QuestionId { get; set; } 
        public Question Question { get; set; }  
    }
}

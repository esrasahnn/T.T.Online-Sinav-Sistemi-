using System.Security.Cryptography.X509Certificates;

namespace TtExam
{
    public class QuestionDto
    {
        public QuestionDto()
        {
            Exams = new List<ExamSectionDto>();
            Answers = new List<AnswerDto>();
            Answers.Add(new AnswerDto());
            Answers.Add(new AnswerDto());
            Answers.Add(new AnswerDto());
            Answers.Add(new AnswerDto());
        }
        public int Id { get; set; }
     
        public int LessonId { get; set; }
        public string Sentence { get; set; }
        public IEnumerable<ExamSectionDto> Exams { get; set; }
        public IList<AnswerDto> Answers { get; set; }
    }
}

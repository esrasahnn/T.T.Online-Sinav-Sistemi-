namespace TtExam.Domain
{
    public class ExamSection
    {
        public ExamSection()
        {
            ExamSectionQuestions = new List<ExamSectionQuestion>();

        }

        public int Id { get; set; }
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
        public Exam Exam { get; set; }  
        public int ExamId { get; set;}
        public ICollection<ExamSectionQuestion> ExamSectionQuestions { get; set; }
    }
}

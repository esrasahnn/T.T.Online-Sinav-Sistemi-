namespace TtExam.Domain
{
    public class Question
    {
        public Question()
        {
            Answers= new List<Answer>();
            ExamSectionQuestions = new List<ExamSectionQuestion>();
        }
        public int Id { get; set; }
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
        public string Sentence { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<ExamSectionQuestion> ExamSectionQuestions { get; set; }
    }
}

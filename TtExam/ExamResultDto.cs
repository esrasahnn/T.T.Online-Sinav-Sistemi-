namespace TtExam
{
    public class ExamResultDto
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public QuestionResultDto[] Questions {get; set; }
        
    }
}

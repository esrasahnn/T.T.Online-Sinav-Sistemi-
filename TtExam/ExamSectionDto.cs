namespace TtExam
{
    public class ExamSectionDto
    {
        public ExamSectionDto()
        {
            Questions = new List<QuestionDto>();
        }

        public int Id { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
    }
}

namespace TtExam
{
    public class ExamDto
    {
        public ExamDto()
        {
            ExamSections = new List<ExamSectionDto>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public ExamStatus Status { get; set; }
        public IEnumerable<ExamSectionDto> ExamSections { get; set; }
    }
}

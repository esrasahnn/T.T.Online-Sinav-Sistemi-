namespace TtExam.Domain
{
    public class Exam
    {
        public Exam()
        {
            ExamSections = new List<ExamSection>();
            StudentExams=new List<StudentExam>();   
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public ExamStatus Status { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }   
        public ICollection<ExamSection> ExamSections { get; set; }
    }
}

namespace TtExam.Domain
{
    public class Lesson
    {
        public Lesson()
        {
            Questions = new List<Question>();
            ExamSections =new List<ExamSection>(); 
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ExamSection> ExamSections { get; set; }  
        public ICollection<Question> Questions { get; set; }

    }
}

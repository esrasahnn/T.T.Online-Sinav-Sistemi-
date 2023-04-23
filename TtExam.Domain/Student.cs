using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TtExam.Domain
{
    public class Student
    {
        public Student()
        {
            StudentExams =new List<StudentExam>(); 
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StudentNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<StudentExam> StudentExams { get; set; }
    }
}

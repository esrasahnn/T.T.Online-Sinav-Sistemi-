using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TtExam
{
    public class EnumHelper
    {
        public static readonly Dictionary<ExamStatus, string> ExanStatusNames = new Dictionary<ExamStatus, string>()
        {
            [ExamStatus.Draft] = "Taslak",
            [ExamStatus.Avaliable] = "Geçerli",
          

        };
    }
}

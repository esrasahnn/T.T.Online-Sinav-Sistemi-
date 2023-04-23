using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Business.Services;
using TtExam.Domain;

namespace TtExam.Business.Validator
{
    internal class ExamValidator
    {
        
        public ValidationResult Validate(Exam exam)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(exam.Name))
            {
                validationResult.AddError("Sınav adı boş geçilemez");
            }
            return validationResult;
        }
    }
}

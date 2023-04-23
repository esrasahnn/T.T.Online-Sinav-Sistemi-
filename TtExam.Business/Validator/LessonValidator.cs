using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Domain;

namespace TtExam.Business.Validator
{
    internal class LessonValidator
    {
        public ValidationResult Validate(Lesson lesson)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(lesson.Name))
            {
                validationResult.AddError("Ders adı boş geçilemez");
            }
            return validationResult;
        }
    }
}

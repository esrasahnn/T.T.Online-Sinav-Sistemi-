using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Domain;

namespace TtExam.Business.Validator
{
    internal class StudentValidator
    {

        public ValidationResult Validate(Student student)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                validationResult.AddError("Adı boş geçilemez");
            }
            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                validationResult.AddError("Soyadı boş geçilemez");
            }
            if (string.IsNullOrWhiteSpace(student.IdentityNumber))
            {
                validationResult.AddError("T.C kimlik no boş geçilemez");
            }
            return validationResult;
        }
    }
}

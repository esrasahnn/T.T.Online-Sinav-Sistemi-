using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Domain;

namespace TtExam.Business.Validator
{
    internal class AnswerValidator
    {
        public ValidationResult Validate(Question question)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(question.Sentence))
            {
                validationResult.AddError("Lütfen soru yazınız!");
            }
            return validationResult;
        }
    }
}

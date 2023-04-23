using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Domain;

namespace TtExam.Business.Validator
{
    internal class QuestionValidator
    {
        public ValidationResult Validate(Question question)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(question.Sentence))
            {
                validationResult.AddError("Lütfen soru yazınız!");
            }

            if(question.Answers.Count(a => a.IsCorrect) != 1)
            {
                validationResult.AddError("Doğru cevap sayısı 1 olmalı");
            }

            return validationResult;
        }
    }
}

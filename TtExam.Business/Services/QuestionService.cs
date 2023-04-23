using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TtExam.Business.Validator;
using TtExam.DataAccess;
using TtExam.Domain;

namespace TtExam.Business.Services
{
    public class QuestionService
    {
        private readonly TtExamContext _context = new TtExamContext();
        private readonly QuestionValidator _validator = new QuestionValidator();
        private readonly LessonService _lessonService= new LessonService();

        public CommandResult Create(QuestionDto questionDto)
        {
            if (questionDto == null) throw new ArgumentNullException(nameof(questionDto));

            try
            {
                var question = MaptoEntity(questionDto);
                var validationResult = _validator.Validate(question);

                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Questions.Add(question);
                _context.SaveChanges();
                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Kayıt hatası !!", ex);
            }
        }
        public CommandResult Update(QuestionDto questionDto)
        {
            try
            {
                var question = MaptoEntity(questionDto);
                var validationResult = _validator.Validate(question);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                var answers = question.Answers;
                question.Answers = default(ICollection<Answer>);
                _context.Questions.Update(question);
                _context.Answers.UpdateRange(answers);
                _context.SaveChanges();
                return CommandResult.Success("Güncelleme başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Güncelleme hatası !!", ex);
            }
        }
        public CommandResult Delete(int id)
        {
            return Delete(new QuestionDto() { Id = id });
        }
        public CommandResult Delete(QuestionDto questionDto)
        {
            try
            {
                var question= MaptoEntity(questionDto);
                _context.Remove(question);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme hatası !!", ex);
            }
        }
        public QuestionDto? GetById(int id)
        {
            try
            {               
               var question = _context.Questions.Include(i=>i.Answers).FirstOrDefault(ques => ques.Id == id);
                var questionDto = MaptoDto(question);
                return questionDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<QuestionDto> GetAll()
        {
            try
            {
                var questionDto = _context.Questions.Select(MaptoDto).ToList();
                return questionDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<QuestionDto>();
            }
        }
        public IEnumerable<QuestionDto> GetQuestionsByLessonId(int lessonId)
        {
            try
            {
                var questionDto = _context.Questions.Where(w => w.LessonId == lessonId).Select(MaptoDto).ToList();
                return questionDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<QuestionDto>();
            }
        }
        public IEnumerable<QuestionSummary> GetSummaries()
        {
            try
            {
                var question = _context.Questions.Select(question => new QuestionSummary()
                {
                    Id= question.Id,
                    LessonId= question.LessonId,
                    LessonName=question.Lesson.Name,
                    Sentence=question.Sentence
                    
                                
                }).ToList();

               return question;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<QuestionSummary>();
            }
        }

        public static Question MaptoEntity(QuestionDto questionDto)
        {
            Question entity = null;
            if (questionDto != null)
            {
                entity = new Question()
                {
                    Id = questionDto.Id,
                    Sentence = questionDto.Sentence,
                    LessonId = questionDto.LessonId,
                    Answers = questionDto.Answers.Select(s=> new Answer { Id = s.Id, IsCorrect  = s.IsCorrect, Text = s.Text, QuestionId = questionDto.Id }).ToList()
                };
            }
            return entity;

        }
        public static QuestionDto MaptoDto(Question question)
        {
            QuestionDto dto = null;
            if (question != null)
            {
                dto = new QuestionDto()
                {
                    Id= question.Id,
                    Sentence=question.Sentence,
                    LessonId=question.LessonId,
                    Answers = question.Answers?.Select(s=> MaptoAnswerDto(s))?.ToList()
                };
            }
            return dto;

        }

        public static AnswerDto MaptoAnswerDto(Answer answer)
        {
            AnswerDto dto = null;
            if (answer != null)
            {
                dto = new AnswerDto()
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect,
                    QuestionId = answer.QuestionId
                };
            }
            return dto;

        }
    }
}

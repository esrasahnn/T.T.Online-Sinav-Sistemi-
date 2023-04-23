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
    public class ExamService
    {
        private readonly TtExamContext _context = new TtExamContext();
        private readonly ExamValidator _validator = new ExamValidator();

        public CommandResult Create(ExamDto examDto)
        {
            if (examDto == null) throw new ArgumentNullException(nameof(examDto));

            try
            {
                var exam = MaptoEntity(examDto);
                var validationResult = _validator.Validate(exam);
                exam.Status = ExamStatus.Draft;

                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Add(exam);
                _context.SaveChanges();
                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Kayıt hatası !!", ex);
            }
        }
        public CommandResult Update(ExamDto examDto)
        {
            try
            {
                var exam = MaptoEntity(examDto);
                var validationResult = _validator.Validate(exam);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Update(exam);
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
            return Delete(new ExamDto() { Id = id });
        }
        public CommandResult Delete(ExamDto examDto)
        {
            try
            {
                var exam = MaptoEntity(examDto);
                _context.Remove(exam);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme hatası !!", ex);
            }
        }
        public ExamDto? GetById(int id, bool isTakeSubEntities = false)
        {
            try
            {
                var examDto = new ExamDto();

                var examContext = _context.Exams.AsQueryable();

                if (isTakeSubEntities)
                {
                    examContext = examContext.Include(e => e.ExamSections)
                                            .ThenInclude(es => es.ExamSectionQuestions)
                                            .ThenInclude(esq => esq.Question)
                                            .ThenInclude(q => q.Answers)
                                            .Include(e => e.ExamSections).ThenInclude(es => es.Lesson);
                }

                examDto = examContext.Select(MaptoDto).FirstOrDefault(Exam => Exam.Id == id);

                return examDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<ExamDto> GetAll()
        {
            try
            {
                var examDto = _context.Exams.Select(MaptoDto).ToList();
                return examDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<ExamDto>();
            }
        }

        public IEnumerable<ExamDto> GetAvaibleAndCurrentExams()
        {
            try
            {
                var examDto = _context.Exams.Where(exam => exam.Status == ExamStatus.Avaliable && exam.Date <= DateTime.Today).Select(MaptoDto).ToList();
                return examDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<ExamDto>();
            }
        }


        public CommandResult CreateExamSection(ExamSectionSummary examSectionSummary)
        {
            if (examSectionSummary == null) throw new ArgumentNullException(nameof(examSectionSummary));

            try
            {
                var examSection = MaptoExamSection(examSectionSummary);
                _context.Add(examSection);
                _context.SaveChanges();

                foreach (var question in examSectionSummary.Questions)
                {
                    var examSectionQuestion = new ExamSectionQuestion { QuestionId = question.Id, ExamSectionId = examSection.Id };
                    _context.Add(examSectionQuestion);
                }
                _context.SaveChanges();

                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Kayıt hatası !!", ex);
            }
        }
        public CommandResult UpdateExamSection(ExamSectionSummary examSectionSummary)
        {
            try
            {
                var examSection = MaptoExamSection(examSectionSummary);
                _context.Update(examSection);
                _context.SaveChanges();

                var oldExamSectionQuestions = _context.ExamSectionQestions.Where(w => w.ExamSectionId == examSection.Id).ToList();
                _context.RemoveRange(oldExamSectionQuestions);

                if (examSectionSummary.Questions != null)
                {

                    foreach (var question in examSectionSummary.Questions)
                    {
                        var examSectionQuestion = new ExamSectionQuestion { QuestionId = question.Id, ExamSectionId = examSection.Id };
                        _context.Add(examSectionQuestion);
                    }
                }
                _context.SaveChanges();

                return CommandResult.Success("Güncelleme başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Güncelleme hatası !!", ex);
            }
        }
        public CommandResult DeleteExamSection(int id)
        {
            return DeleteExamSection(new ExamSectionSummary() { Id = id });
        }
        public CommandResult DeleteExamSection(ExamSectionSummary examSectionSummary)
        {
            try
            {
                var examSec = MaptoExamSection(examSectionSummary);
                var isExstExamSectionQuestions = _context.ExamSectionQestions.Any(w => w.ExamSectionId == examSectionSummary.Id);
                if (isExstExamSectionQuestions)
                {
                    return CommandResult.Error("Bu sınav bölümü altında tanımlı sorular olduğu için bu kayıt silinemez");
                }
                else
                {
                    _context.Remove(examSec);
                    _context.SaveChanges();
                    return CommandResult.Success("Silme işlemi başarılı");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme hatası !!", ex);
            }
        }
        public ExamSectionSummary? GetExamSectionById(int id)
        {
            try
            {
                var examDto = _context.ExamSections.Include(es => es.ExamSectionQuestions).ThenInclude(esq => esq.Question).Include(es => es.Lesson).Select(MaptoExamSectionSummary).FirstOrDefault(ExamSec => ExamSec.Id == id);
                return examDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<ExamSectionSummary> GetExamSectionsByExamId(int ExamId)
        {
            try
            {
                var exam = _context.ExamSections.Include(es => es.Lesson).Include(ques => ques.ExamSectionQuestions).ThenInclude(esq => esq.Question).Where(e => e.ExamId == ExamId).Select(MaptoExamSectionSummary).ToList();
                return exam;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<ExamSectionSummary>();
            }
        }
        public IEnumerable<ExamSectionSummary> GetAllExamSection()
        {
            try
            {
                var examSec = _context.ExamSections.Select(MaptoExamSectionSummary).ToList();
                return examSec;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<ExamSectionSummary>();
            }
        }

        internal static ExamSectionSummary MaptoExamSectionSummary(ExamSection examSection)
        {
            ExamSectionSummary entity = null;
            if (examSection != null)
            {
                entity = new ExamSectionSummary()
                {
                    Id = examSection.Id,
                    LessonName = examSection.Lesson.Name,
                    LessonId = examSection.LessonId,
                    ExamId = examSection.ExamId,
                    Questions = examSection.ExamSectionQuestions?.Select(s => QuestionService.MaptoDto(s.Question)),
                };
            }
            return entity;

        }

        internal static ExamSectionDto MaptoExamSectionDto(ExamSection examSection)
        {
            ExamSectionDto entity = null;
            if (examSection != null)
            {
                entity = new ExamSectionDto()
                {
                    Id = examSection.Id,
                    LessonId = examSection.Lesson?.Id ?? 0,
                    LessonName = examSection.Lesson?.Name,
                    Questions = examSection.ExamSectionQuestions?.Select(s => QuestionService.MaptoDto(s.Question)),
                };
            }
            return entity;

        }

        internal static ExamSection MaptoExamSection(ExamSectionSummary examSectionSummary)
        {
            ExamSection entity = null;
            if (examSectionSummary != null)
            {
                entity = new ExamSection()
                {
                    Id = examSectionSummary.Id,
                    ExamId = examSectionSummary.ExamId,
                    LessonId = examSectionSummary.LessonId,
                    

                };
            }
            return entity;

        }
        internal static Exam MaptoEntity(ExamDto examDto)
        {
            Exam entity = null;
            if (examDto != null)
            {
                entity = new Exam()
                {
                    Id = examDto.Id,
                    Name = examDto.Name,
                    Date = examDto.Date,
                    Status = examDto.Status,
                  
                };
            }
            return entity;

        }
        internal static ExamDto MaptoDto(Exam exam)
        {
            ExamDto dto = null;
            if (exam != null)
            {
                dto = new ExamDto()
                {
                    Id = exam.Id,
                    Name = exam.Name,
                    Date = exam.Date,
                    Status = exam.Status,
                    ExamSections = exam.ExamSections?.Select(MaptoExamSectionDto)
                };
            }
            return dto;

        }

        public CommandResult ChangeStatus(int examId, ExamStatus status)
        {
            try
            {
                var exam = _context.Exams.Single(w => w.Id == examId);
                exam.Status = status;
                _context.SaveChanges();

                return CommandResult.Success("Güncelleme başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Güncelleme hatası !!", ex);

            }


        }

        public CommandResult EvaluateExam(ExamResultDto examResultDto)
        {
            var student = _context.Students.SingleOrDefault(w => w.Id == examResultDto.StudentId);

            if(student is null)
            {
                return CommandResult.Error($"Öğrenci kaydına ulaşılamadı");
            }

            var exam = _context.Exams.Include(e => e.ExamSections)
                        .ThenInclude(es => es.ExamSectionQuestions).ThenInclude(esq => esq.Question).ThenInclude(q => q.Answers)
                        .SingleOrDefault(w => w.Id == examResultDto.ExamId && w.Status == ExamStatus.Avaliable && w.Date <= DateTime.Today);

            if (exam is null)
            {
                return CommandResult.Error($"Sınav kaydına ulaşılamadı");
            }

            var studentExam = _context.StudentExams.SingleOrDefault(se => se.ExamId == examResultDto.ExamId && se.StudentId == examResultDto.StudentId);

            if (studentExam != null)
            {
                return CommandResult.Error($"Daha önce bu sınava girdiniz. Puanınız: {studentExam.Point:0.##}");
            }


            double examPoint = 0;
            if(examResultDto.Questions != null)
            {
                var examQuestions = exam.ExamSections.SelectMany(s => s.ExamSectionQuestions.Select(ss => ss.Question)).ToList();

                var eachQuestionPoint = 100 / examQuestions.Count;

                foreach (var question in examResultDto.Questions)
                {
                    var existQuestion = examQuestions.SingleOrDefault(q => q.Id == question.Id);

                    if(existQuestion is null)
                    { continue; }

                    var existAnswer = existQuestion.Answers.SingleOrDefault(a => a.Id == question.Answer.Id);

                    if(existAnswer is null)
                    { continue; }

                    if(existAnswer.IsCorrect)
                    {
                        examPoint += eachQuestionPoint;
                    }
                }

                studentExam = new StudentExam 
                { 
                    ExamId = examResultDto.ExamId, 
                    StudentId = examResultDto.StudentId, 
                    Point = examPoint 
                };
                _context.Add(studentExam);
                _context.SaveChanges();
            }
            return CommandResult.Success($"Sınavdan aldığınız puan: {studentExam.Point:0.##}");

        }
    }
}

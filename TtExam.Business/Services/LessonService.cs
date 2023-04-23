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
    public class LessonService
    {
        private readonly TtExamContext _context = new TtExamContext();
        private readonly LessonValidator  _validator = new LessonValidator();

        public CommandResult Create(LessonDto lessonDto)
        {
            if (lessonDto == null) throw new ArgumentNullException(nameof(lessonDto));

            try
            {
                var lesson = MaptoEntity(lessonDto);
                var validationResult = _validator.Validate(lesson);

                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Add(lesson);
                _context.SaveChanges();
                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Kayıt hatası !!", ex);
            }
        }
        public CommandResult Update(LessonDto lessonDto)
        {
            if (lessonDto == null)
                throw new ArgumentNullException(nameof(lessonDto));
            try
            {
                var lesson = MaptoEntity(lessonDto);
                var validationResult = _validator.Validate(lesson);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Update(lesson);
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
            return Delete(new LessonDto() { Id = id });
        }
        public CommandResult Delete(LessonDto lessonDto)
        {
            try
            {
                var lesson = MaptoEntity(lessonDto);
                _context.Remove(lesson);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme hatası !!", ex);
            }
        }
        public LessonDto? GetById(int id)
        {
            try
            {
                var lessonDto = _context.Lessons.Select(MaptoDto).FirstOrDefault(Lesson => Lesson.Id == id);
                return lessonDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<LessonDto> GetAll()
        {
            try
            {
                var lessonDto = _context.Lessons.Select(MaptoDto).ToList();
                return lessonDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<LessonDto>();
            }
        }
       
   
        public static Lesson? MaptoEntity(LessonDto lessonDto)
        {
            Lesson entity = null;
            if (lessonDto != null)
            {
                entity = new Lesson()
                {
                    Id = lessonDto.Id,
                    Name = lessonDto.Name,
                    Description = lessonDto.Description
                };
            }
            return entity;
           
        }
        public static LessonDto MaptoDto(Lesson lesson)
        {
            LessonDto dto = null;
            if (lesson != null)
            {
                dto = new LessonDto()
                {
                    Id = lesson.Id,
                    Name = lesson.Name,
                    Description = lesson.Description
                };
            }
            return dto;
            
        }
    }
}

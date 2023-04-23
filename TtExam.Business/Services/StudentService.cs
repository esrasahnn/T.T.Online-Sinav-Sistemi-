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
    public class StudentService
    {
        private readonly TtExamContext _context = new TtExamContext();
        private readonly StudentValidator _validator = new StudentValidator();

        public CommandResult Create(StudentDto studentDto)
        {
            if (studentDto == null) throw new ArgumentNullException(nameof(studentDto));

            try
            {
                var student = MaptoEntity(studentDto);
                var validationResult = _validator.Validate(student);

                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Add(student);
                _context.SaveChanges();
                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Kayıt hatası !!", ex);
            }
        }
        public CommandResult Update(StudentDto studentDto)
        {
            try
            {
                var student = MaptoEntity(studentDto);
                var validationResult = _validator.Validate(student);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Update(student);
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
            return Delete(new StudentDto() { Id = id });
        }
        public CommandResult Delete(StudentDto studentDto)
        {
            try
            {
                var student = MaptoEntity(studentDto);
                _context.Remove(student);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme hatası !!", ex);
            }
        }
        public StudentDto? GetById(int id)
        {
            try
            {
                var studentDto = _context.Students.Select(MaptoDto).FirstOrDefault(student => student.Id == id);
                return studentDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<StudentDto> GetAll()
        {
            try
            {
                var studentDto = _context.Students.Select(MaptoDto).ToList();
                return studentDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<StudentDto>();
            }
        }


        public static Student MaptoEntity(StudentDto studentDto)
        {
            return new Student()
            {
                Id = studentDto.Id,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                IdentityNumber = studentDto.IdentityNumber,
                DateOfBirth = studentDto.DateOfBirth,
                StudentNumber = studentDto.StudentNumber,
                PhoneNumber = studentDto.PhoneNumber,


            };
        }
        public static StudentDto MaptoDto(Student student)
        {
            return new StudentDto()
            {
               Id= student.Id,  
               FirstName = student.FirstName,   
               LastName = student.LastName,
               IdentityNumber = student.IdentityNumber,
               DateOfBirth = student.DateOfBirth,
               StudentNumber= student.StudentNumber,
               PhoneNumber= student.PhoneNumber,
               
            };
        }

        public StudentDto GetStudentByIdentityNumber(string IdentityNumber)
        {
            try
            {
                var studentDto = _context.Students.Select(MaptoDto).FirstOrDefault(student => student.IdentityNumber == IdentityNumber);
                return studentDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
    }
}

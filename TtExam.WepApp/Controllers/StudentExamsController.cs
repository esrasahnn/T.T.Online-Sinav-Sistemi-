using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TtExam.Business;
using TtExam.Business.Services;
using TtExam.WepApp.Helpers;
using TtExam.WepApp.Models;

namespace TtExam.WepApp.Controllers
{
    public class StudentExamsController : Controller
    {
        
        private readonly StudentService _studentService = new StudentService();
        private readonly ExamService _examService = new ExamService();  

        public IActionResult Index()
        {
            
            return View("Student");
        }

        public IActionResult StudentSearch(string IdentityNumber)
        {
            var student = _studentService.GetStudentByIdentityNumber(IdentityNumber);
            var exams = _examService.GetAvaibleAndCurrentExams();
            ViewBag.ExamName = new SelectList(exams, "Id", "Name");

            if (student == null)
                TempData["ErrorMessage"] = "Öğrenci bulunamadı";
            else

                ViewBag.Student = student;

            return View("Student");
        }

        
        public IActionResult Exam(int id, int studentId)
        {
            #region asda
            #endregion
            var examDto = _examService.GetById(id, true);
            ViewBag.StudentId = studentId;
            return View("Exam", examDto);
        }


        [HttpPost]
        public IActionResult ExamFinish(ExamResultDto examResultModel)
        {
            var result = _examService.EvaluateExam(examResultModel);

            if(result.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = result.Message;
            }
            else
            {
                TempData[Keys.ErrorMessage] = result.Message;
            }

            return RedirectToAction("Exam", new { id= examResultModel.ExamId, studentId = examResultModel.StudentId });
        }

        [HttpPost]
        public IActionResult Finish(int id, string submit)
        {
            
            return View("{controller=Home}/{action=Index}");
        }

    }


}

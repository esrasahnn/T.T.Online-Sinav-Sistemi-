using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TtExam.Business.Services;
using TtExam.Domain;
using TtExam.WepApp.Helpers;

namespace TtExam.WepApp.Controllers
{
    public class ExamSectionsController : Controller
    {
        private readonly ExamService _examService=new ExamService();    
        private readonly LessonService _lessonService =new LessonService();
        private readonly QuestionService _questionService = new QuestionService();

        public IActionResult Index(int ExamId)
        {
            var exam=_examService.GetById(ExamId);  
            if(exam == null) { 
             return RedirectToAction("Index", "Exams");
            }
            ViewBag.ExamName = exam.Name;
            ViewBag.ExamId = exam.Id;
            var examSections = _examService.GetExamSectionsByExamId(ExamId);
            return View(examSections);
        }

        public IActionResult Create(int Id)
        {
            var allLessons = _lessonService.GetAll();
            ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
            return View(new ExamSectionSummary() { ExamId=Id });
        }

        [HttpPost]
        public IActionResult Create(ExamSectionSummary examSectionSummary)
        {
            var commadResult = _examService.CreateExamSection(examSectionSummary);
            if (commadResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commadResult.Message;
                return RedirectToAction("Index", new
                {
                    examId = examSectionSummary.ExamId
                });
            }
            else
            {
                var allLessons = _lessonService.GetAll();
                ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
                TempData[Keys.ErrorMessage] = commadResult.Message;
                return View(examSectionSummary);
            }
        }
        public IActionResult Update(int id)
        {
            var examSection = _examService.GetExamSectionById(id);
            if (examSection != null)
            {
                var allLessons = _lessonService.GetAll();
                ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
                ViewBag.Questionlist = _questionService.GetQuestionsByLessonId(examSection.LessonId);
                return View(examSection);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index", new { examId = examSection.ExamId });
            }
        }
        [HttpPost]
        public IActionResult Update(ExamSectionSummary examSectionSummary)
        {
            var commandResult = _examService.UpdateExamSection(examSectionSummary);
            if (commandResult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandResult.Message;
                return RedirectToAction("Index", new { examId = examSectionSummary.ExamId });
            }
            else
            {
                var allLessons = _lessonService.GetAll();
                ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
                ViewBag.Questionlist = _questionService.GetQuestionsByLessonId(examSectionSummary.LessonId);
                ViewData[Keys.ErrorMessage] = commandResult.Message;
                return View(examSectionSummary);
            }
        }

        public IActionResult Delete(int id)
        {
            var examSection = _examService.DeleteExamSection(id);
            if (examSection != null)
            {
               
                return View(examSection);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(ExamSectionSummary examSectionSummary)
        {
            var commandResult = _examService.DeleteExamSection(examSectionSummary);
            if (commandResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commandResult.Message;
            }
            else
            {
                TempData[Keys.ErrorMessage] = commandResult.Message;
            }
            return RedirectToAction("Index", new { examId = examSectionSummary.ExamId });
        }
    }
}

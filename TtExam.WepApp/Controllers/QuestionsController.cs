using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TtExam.Business.Services;
using TtExam.DataAccess;
using TtExam.Domain;
using TtExam.WepApp.Helpers;

namespace TtExam.WepApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly QuestionService _questionService = new QuestionService();
        private readonly LessonService _lessonService= new LessonService();
        private readonly TtExamContext _context = new();

        public IActionResult Index()
        {
            var questions = _questionService.GetSummaries();
            return View(questions);
        }

        public IActionResult Create()
        {
            var allLessons= _lessonService.GetAll();
            ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
            return View(new QuestionDto());
        }

        [HttpPost]
        public IActionResult Create(QuestionDto question)
        {
            var commadResult = _questionService.Create(question);
            if (commadResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commadResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                var allLessons = _lessonService.GetAll();
                ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
                TempData[Keys.ErrorMessage] = commadResult.Message;
                return View(question);
            }
        }
        public IActionResult Update(int id)
        {
            var question = _questionService.GetById(id);
            if (question != null)
            {
               
                var allLessons = _lessonService.GetAll();
                ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
                return View(question);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(QuestionDto question)
        {
            var commandResult = _questionService.Update(question);
            if (commandResult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                
                var allLessons = _lessonService.GetAll();
                ViewBag.LessonSelectList = new SelectList(allLessons, "Id", "Name");
                ViewData[Keys.ErrorMessage] = commandResult.Message;
                return View(question);
            }
        }

        public IActionResult Delete(int id)
        {
            var question = _questionService.Delete(id);
            if (question != null)
            {
                ViewBag.LessonSelectList=_lessonService.GetAll();
                return View(question);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(QuestionDto question)
        {
            var commandResult = _questionService.Delete(question);
            if (commandResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commandResult.Message;
            }
            else
            {
                TempData[Keys.ErrorMessage] = commandResult.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult QuestionsByLesson(int lessonId) 
        {
            var questions = _questionService.GetQuestionsByLessonId(lessonId);

            return Json(questions);
        }
    }
}

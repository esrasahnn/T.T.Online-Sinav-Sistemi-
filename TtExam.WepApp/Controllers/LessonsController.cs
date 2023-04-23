using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TtExam.Business.Services;
using TtExam.WepApp.Helpers;

namespace TtExam.WepApp.Controllers
{
    public class LessonsController : Controller
    {
        private readonly LessonService _lessonService = new LessonService();

        public IActionResult Index()
        {
            var lessons = _lessonService.GetAll();
            return View(lessons);
        }

        public IActionResult Create()
        {
            var lessons = _lessonService.GetAll();
            return View(new LessonDto());
        }

        [HttpPost]
        public IActionResult Create(LessonDto lesson)
        {
            var commadResult = _lessonService.Create(lesson);
            if (commadResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commadResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                var lessons = _lessonService.GetAll();
                TempData[Keys.ErrorMessage] = commadResult.Message;
                return View(lesson);
            }
        }
        public IActionResult Update(int id)
        {
            var lesson = _lessonService.GetById(id);
            if (lesson != null)
            {
               //var lessons = _lessonService.GetAll();
               // ViewBag.Lessons = new SelectList(lessons, "Id", "Name");
                return View(lesson);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(LessonDto lesson)
        {
            var commandResult = _lessonService.Update(lesson);
            if (commandResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
               // var lessons = _lessonService.GetAll();
                //ViewBag.Lessons = new SelectList(lessons, "Id", "Name");
                ViewData[Keys.ErrorMessage] = commandResult.Message;
                return View(lesson);
            }
        }

        public IActionResult Delete(int id)
        {
            var lesson = _lessonService.Delete(id);
            if (lesson != null)
            {

                return View(lesson);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(LessonDto lesson)
        {
            var commandResult = _lessonService.Delete(lesson);
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

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TtExam.Business.Services;
using TtExam.WepApp.Helpers;

namespace TtExam.WepApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService = new StudentService();

        public IActionResult Index()
        {
            var students = _studentService.GetAll();
            return View(students);
        }

        public IActionResult Create()
        {
            var students = _studentService.GetAll();
            return View(new StudentDto());
        }

        [HttpPost]
        public IActionResult Create(StudentDto student)
        {
            var commadResult = _studentService.Create(student);
            if (commadResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commadResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                var students = _studentService.GetAll();
                TempData[Keys.ErrorMessage] = commadResult.Message;
                return View(students);
            }
        }
        public IActionResult Update(int id)
        {
            var student = _studentService.GetById(id);
            if (student != null)
            {
                return View(student);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(StudentDto student)
        {
            var commandResult = _studentService.Update(student);
            if (commandResult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {

                ViewData[Keys.ErrorMessage] = commandResult.Message;
                return View(student);
            }
        }

        public IActionResult Delete(int id)
        {
            var student = _studentService.Delete(id);
            if (student != null)
            {

                return View(student);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(StudentDto student)
        {
            var commandResult = _studentService.Delete(student);
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

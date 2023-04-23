using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TtExam.Business.Services;
using TtExam.WepApp.Helpers;

namespace TtExam.WepApp.Controllers
{
    public class ExamsController : Controller
    {

        private readonly ExamService _examService = new ExamService();

        public IActionResult Index()
        {
            var exams = _examService.GetAll();
            return View(exams);
        }

        public IActionResult Create()
        {
            var exams = _examService.GetAll();
            ViewBag.ExamName = new SelectList(exams, "Id", "Name");
            return View(new ExamDto());
        }

        [HttpPost]
        public IActionResult Create(ExamDto examDto)
        {
            var commadResult = _examService.Create(examDto);
            if (commadResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commadResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                var exams = _examService.GetAll();
                ViewBag.ExamName = new SelectList(exams, "Id", "Name");
                TempData[Keys.ErrorMessage] = commadResult.Message;
                return View(examDto);
            }
        }
        public IActionResult Update(int id)
        {
            var exam =_examService.GetById(id);
            if (exam != null)
            {
               
                return View(exam);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(ExamDto exam)
        {
            var commandResult = _examService.Update(exam);
            if (commandResult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                
                ViewData[Keys.ErrorMessage] = commandResult.Message;
                return View(exam);
            }
        }

        public IActionResult Delete(int id)
        {
            var exam = _examService.Delete(id);
            if (exam != null)
            {
                
                return View(exam);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(ExamDto exam)
        {
            var commandResult = _examService.Delete(exam);
            if (commandResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commandResult.Message;
            }
            else
            {
                TempData[Keys.ErrorMessage] = $"Bu sınava kayıtlı öğrenciler olduğundan silinemedi !";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Activate(int id)
        {
            var exam = _examService.GetById(id);
            if (exam != null)
            {
               var result =   _examService.ChangeStatus(id, ExamStatus.Avaliable);
                if(result.IsSuccess)
                {
                    TempData[Keys.SuccessMessage] = result.Message;  
                }
                else
                { TempData["ErrorMessage"] = $"{exam.Name} aktif edilemedi"; }
               
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
               
            }

            return RedirectToAction("Index");
        }

        public IActionResult Passive(int id)
        {
            var exam = _examService.GetById(id);
            if (exam != null)
            {
                var result = _examService.ChangeStatus(id, ExamStatus.Draft);
                if (result.IsSuccess)
                {
                    TempData[Keys.SuccessMessage] = result.Message;
                }
                else
                { TempData["ErrorMessage"] = $"{exam.Name} pasif edilemedi"; }

            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";

            }

            return RedirectToAction("Index");
        }
    }
}

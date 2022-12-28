using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Web.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            var _uploadedFiles = Request.Form.Files;
            return View();
        }

    }
}

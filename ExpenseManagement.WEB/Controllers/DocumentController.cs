using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Web.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Web.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

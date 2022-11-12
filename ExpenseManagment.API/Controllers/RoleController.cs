using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagment.API.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

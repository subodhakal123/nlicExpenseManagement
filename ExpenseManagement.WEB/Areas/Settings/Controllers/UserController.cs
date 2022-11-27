using ExpenseManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Web.Areas.Settings.Controllers
{
    [Area("Settings")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            var model = new List<UserModel>()
            {
                new UserModel(){ FirstName = "ram",LastName = "khadka", Mobile="987987", EmailId="ram@gmail.com"},
                new UserModel(){ FirstName = "kram",LastName = "akhadka", Mobile="23987987", EmailId="aram@gmail.com"},
                new UserModel(){ FirstName = "bram",LastName = "bkhadka", Mobile="23987987", EmailId="bram@gmail.com"}
            };
            return View(model);
        }
        public IActionResult Create()
        {
            var model = new UserModel()
            {
                FirstName = "ram",LastName = "khadka", Mobile="987987", EmailId="ram@gmail.com"
            };
            return View(model);
        }
        public IActionResult Edit()
        {
            var model = new UserModel()
            {
                FirstName = "ram",
                LastName = "khadka",
                Mobile = "987987",
                EmailId = "ram@gmail.com"
            };
            return View(model);
        }
        public IActionResult Delete()
        {
            var model = new UserModel()
            {
                FirstName = "ram",
                LastName = "khadka",
                Mobile = "987987",
                EmailId = "ram@gmail.com"
            };
            return View(model);
        }
        public IActionResult Details()
        {
            var model = new UserModel()
            {
                FirstName = "ram",
                LastName = "khadka",
                Mobile = "987987",
                EmailId = "ram@gmail.com"
            };
            return View(model);
        }
    }
}

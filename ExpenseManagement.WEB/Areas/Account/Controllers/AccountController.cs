using ExpenseManagement.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseManagement.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        
        

        UserModel model = new UserModel()
        {
            UserId = 1,
            FirstName = "firstname",
            LastName = "lastname",
            EmailId = "email@id.com",
            Mobile = "98798789798",
            Role = "admin"
        };
        public IActionResult Index()
        {
            return View();
        }
        //public ActionResult LogIn()
        //{
        //    return PartialView(new UserModel());
        //}
        public ActionResult LogIn(AppUserModel usr)
        {
            UserModel model1 = new UserModel();
            model1 = model;

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,model1.FirstName),
                new Claim(ClaimTypes.Role,model1.Role)
            },CookieAuthenticationDefaults.AuthenticationScheme,ClaimTypes.Name,ClaimTypes.Role);

            var principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;

            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal,new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = true});
            return View();
        }

        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}

using ExpenseManagement.Model;
using ExpenseManagement.Web.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;

namespace ExpenseManagement.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        public WebApiService ws;
        public AccountController(){
            ws = new WebApiService(null);
        }
        public async Task<ActionResult> LogIn(UserDto model)
        {
            try
            {
                if (model.login == null) { return Redirect("/"); }

                model.register = new UserViewModel();

                AppUserModel usr = new AppUserModel();
                usr.UserName = model.login.Username;
                usr.Password = model.login.Password;
                usr.Email = model.login.Username;

                RestResponse userModel = ws.Authenticate(usr);
                
                UserModel response = JsonConvert.DeserializeObject<UserModel>(userModel.Content);
                if(response.UserName != null)
                {
                    var identity = new ClaimsIdentity(new[]
                                         {
                                             new Claim(ClaimTypes.Name,model.login.Username),
                                             new Claim("AccessToken", response.access_token)
                                         }
                                         , CookieAuthenticationDefaults.AuthenticationScheme
                                         , ClaimTypes.Name
                                         , ClaimTypes.Role);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    Thread.CurrentPrincipal = principal;
                    HttpContext.User = principal;

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = true });
                }
                else
                {
                    //not authenticated
                    return Redirect("/");
                }

            }
            catch (Exception ex)
            {
                return Redirect("/");
            }
            return Redirect("/Expense");
        }

        public ActionResult LogOut()
        {
            var cde = User.Claims;
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}

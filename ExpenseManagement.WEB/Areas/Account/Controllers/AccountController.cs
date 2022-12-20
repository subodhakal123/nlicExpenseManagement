using ExpenseManagement.Model;
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
        public async Task<ActionResult> LogIn(UserDto model)
        {
            try
            {
                AppUserModel usr = new AppUserModel();
                usr.UserName = model.login.Username;
                usr.Password = model.login.Password;
                usr.Email = model.login.Username;

                model.register = new UserViewModel();
                
                var client = new RestClient();
                var request = new RestRequest();
                request.Method = Method.Post;
                request.Resource = "https://localhost:7250/api/Account/Token";
                request.AddJsonBody(usr);
                UserModel response =  await client.PostAsync<UserModel>(request);
                //IRestResponse response = client.Execute(request);
                if(response.UserName != null){
                           var identity = new ClaimsIdentity(new[]
                                                {
                                                new Claim(ClaimTypes.Name,model.login.Username)
                                            }, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                           var principal = new ClaimsPrincipal(identity);
                           Thread.CurrentPrincipal = principal;

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

            }
            return Redirect("/Expense");
        }

        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}

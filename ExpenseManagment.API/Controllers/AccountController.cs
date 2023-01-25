using ExpenseManagement.BLL.Account;
using ExpenseManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService _as;
        public AccountController()
        {
            _as = new AccountService();
        }

        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        public UserModel Token([FromBody] AppUserModel usr)
        {
            UserModel model = _as.ValidateUser(usr.UserName,usr.Password);
            if (model.UserName == null || !string.IsNullOrEmpty(model.ErrorMessage))
            {
                model.ErrorMessage = model.ErrorMessage;
            }
            else
            {
                model.access_token = _as.CreateToken(usr.UserName, usr.Password);
                model.CultureCD = model.CultureCD ?? "ne-NP";
            }
            
            return model;
        }
    }
}

using ExpenseManagement.BLL.Account;
using ExpenseManagement.Model;
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
        public UserModel Token([FromBody] AppUserModel usr)
        {
            //usr.UserName = "string";
            //usr.Password = "password";
            UserModel model = _as.ValidateUser(usr.UserName,usr.Password);
            if (model.UserName == null || !string.IsNullOrEmpty(model.ErrorMessage))
            {
                model.ErrorMessage = "either username is null or some error message is encountered";
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

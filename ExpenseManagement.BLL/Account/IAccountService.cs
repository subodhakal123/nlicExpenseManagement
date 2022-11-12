using ExpenseManagement.BLL.Base;
using ExpenseManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Account
{
    public interface IAccountService: IBaseService
    {
        string CreateToken(string username, string password);

        UserModel ValidateUser(string username, string password);

    }
}

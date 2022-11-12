using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ErrorMessage { get; set; }
        public string access_token { get; set; }
        public string CultureCD { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }

    }
    public class UserRolesNagivationMenu
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
    }
}

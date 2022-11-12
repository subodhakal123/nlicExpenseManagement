using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class signIn
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserDto
    {
        public signIn login { get; set; }
        public UserViewModel register { get; set; }
    }

    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string retMsg { get; set; }
    }
}

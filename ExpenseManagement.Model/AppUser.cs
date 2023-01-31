using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal): base(principal)
        {
        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }
        public string AccessToken
        {
            get
            {
                return this.FindFirst("AccessToken").Value;
            }
        }
    }
}

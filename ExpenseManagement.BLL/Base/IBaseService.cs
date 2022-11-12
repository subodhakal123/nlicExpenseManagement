using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Base
{
    public interface IBaseService
    {
        int CurrentPersonId { get; set; }
        string CurrentCultureCode { get; set; }
    }
}

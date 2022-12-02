using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpenseManagement.Model.Common.DropDownModel;

namespace ExpenseManagement.BLL.Common
{
    public interface IDropDownService
    {
        public List<DropDown> GetDropDowns(DropDownCallParameter callParameter);
    }
}

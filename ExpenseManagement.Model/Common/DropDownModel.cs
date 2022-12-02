using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Common
{
    public class DropDownModel
    {
        public class DropDown
        {
            public string ddlVal { get; set; } = null;
            public string ddlText { get; set; } = null;
            public string ddlTextNp { get; set; } = null;
        }
        public class DropDownCallParameter
        {
            public string mode { get; set; } = null;
            public string condition1 { get; set; } = null;
            public string condition2 { get; set; } = null;
        }
    }
}

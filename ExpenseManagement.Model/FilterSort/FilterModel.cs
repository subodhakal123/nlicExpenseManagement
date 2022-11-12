using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class FilterModel
    {
        public FilterModel()
        {
            Filters = new List<FilterParameterModel>();
        }
        public List<FilterParameterModel> Filters { get; set; }
        public string Logic { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class FilterParameterModel
    {
        public string Operator { get; set; }
        public string Value { get; set; }
        public string Field { get; set; }
        public string Logic { get; set; }
        public IEnumerable<FilterParameterModel> Filters { get; set; }
    }
}

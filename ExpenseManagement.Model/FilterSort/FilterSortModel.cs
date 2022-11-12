using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class FilterSortModel
    {
        public FilterSortModel()
        {
            Filter = new FilterModel();
            sort = new List<SortModel>();
        }

        public FilterModel Filter { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
        public List<SortModel> sort { get; set; }

        public int UserId { get; set; }
        public string CultureCode { get; set; }

    }
}

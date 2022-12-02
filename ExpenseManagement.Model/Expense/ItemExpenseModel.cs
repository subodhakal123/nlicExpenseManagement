using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Expense
{
    public class ItemExpenseModel
    {
        public ExpenseModel Expense { get; set; }
        public List<ItemModel> Item { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Expense
{
    public class ItemExpenseModel
    {
        public int ExpenseId { get; set; }
        public List<ItemModel> Item { get; set; }
        public bool IsRecommended { get; set; }
        public int DepartmentId { get; set; }
        public string Recommender { get; set; }
        public float TotalAmount { get; set; }
        public string Comment { get; set; }

    }
}

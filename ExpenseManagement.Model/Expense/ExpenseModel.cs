using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Expense
{
    public class ExpenseModel
    {
        public int ExpenseId { get; set; }
        public string BranchName { get; set; }
        public string ExpenseType { get; set; }
        public string ExpenseSubType { get; set; }
        public string Comment { get; set; }
        public bool IsRecommended { get; set; }
        public string DepartmentName { get; set; }
        public float ExpenseAmount { get; set;} = 0;

        public bool IsDeleted { get; set; }
    }
}

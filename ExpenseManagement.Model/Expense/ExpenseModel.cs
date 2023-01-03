using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Expense
{
    public class ExpenseModel
    {
        public int? ExpenseId { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public string? BranchName { get; set; }
        public string? ExpenseType { get; set; }
        public string? ExpenseSubType { get; set; }
        public float? Amount { get; set; }
        public float? TotalAmount { get; set; }
        public bool? IsRecommended { get; set; }
        public string? DepartmentName { get; set; }
        public string? Comment { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsApproved { get; set; }
    }
    public class SaveExpense
    {
        public int ExpenseId { get; set; }
        public string retMsg { get; set; }
    }
}

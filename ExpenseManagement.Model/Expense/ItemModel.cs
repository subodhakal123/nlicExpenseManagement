using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Expense
{
    public class ItemModel
    {
        public int ItemId {get; set;}
        public int ExpenseId {get; set;}
        public string ItemName {get; set;}
        public string ExpenseType {get; set;}
        public string ExpenseSubType { get; set; }
        public float ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
        public float ItemAmount {get; set;}
    }

    public class ItemViewModel
    {
        public string ExpenseTitle { get; set; }
        public string ExpenseBy { get; set; }
        public string BranchName { get; set; }
        public string AppliedBy { get; set; }
        public string Status { get; set; }
        public bool IsRecommended {get; set;}
        public int DepartmentName { get; set;}
        public string Recommender { get; set;}
        public string Comment { get; set;}
        public float TotalAmount { get; set;}
        public int ItemId { get; set; }
        public int ExpenseId { get; set; }
        public string ItemName { get; set; }
        public string ExpenseType { get; set; }
        public string ExpenseSubType { get; set; }
        public float ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
        public float ItemAmount { get; set; }
    }
}

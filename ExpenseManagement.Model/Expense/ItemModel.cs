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
        public string? ItemName {get; set;}
        public string? ItemType  {get; set;}
        public string? ItemDesc  {get; set;}
        public float? ItemPrice  {get; set;}
        public int? ItemQuantity {get; set;}
        public float? ItemAmount {get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.Expense
{
    public class ApproveRequestModel
    {
        public int ExpenseId { get; set; }
        public string Username { get; set; }
    }

    public class RequestApproval
    {
        public int ExpenseId { get; set; }
        public int ForwardTo { get; set; }
    }
}

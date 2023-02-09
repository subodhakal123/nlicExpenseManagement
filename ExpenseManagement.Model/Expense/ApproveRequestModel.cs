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
        public int UserId { get; set; }
    }

    public class RequestApproval
    {
        public int ExpenseId { get; set; }
        public int userId { get; set; }
        public int ForwardTo { get; set; }
    }
}

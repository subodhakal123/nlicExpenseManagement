using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Expense
{
    public interface IExpenseService
    {
        ArrayList GetAllExpense(FilterSortModel model);
        ItemExpenseViewModel GetExpenseById(GetExpenseById model);
        SaveExpense SaveExpense(ItemExpenseModel model);
        string DeleteExpense(int ExpenseId);
        string ApproveExpense(ApproveRequestModel model);
        string ApprovalRequest(RequestApproval model);
    }
}

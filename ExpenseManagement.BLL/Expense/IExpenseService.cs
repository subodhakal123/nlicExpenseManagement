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
        ItemExpenseModel GetExpenseById(int ExpenseId);
        SaveExpense SaveExpense(ItemExpenseModel model);
        string DeleteExpense(int ExpenseId);
        string ApproveExpense(ApproveRequestModel model);
    }
}

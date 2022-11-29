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
        ExpenseModel GetExpenseById(int ExpenseId);
        string SaveExpense(ExpenseModel model);
        string DeleteExpense(int ExpenseId);
    }
}

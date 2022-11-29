using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManagement.BLL.Item
{
    public interface IItemService
    {
        ArrayList GetAllItem(FilterSortModel model);
        ItemModel GetItemById(int ItemId);
        string SaveItem(ItemModel model);
        string DeleteItem(int ItemId);
    }
}

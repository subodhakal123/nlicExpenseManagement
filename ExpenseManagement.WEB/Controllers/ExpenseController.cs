using ExpenseManagement.Model.Expense;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace ExpenseManagement.Web.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditExpense(ItemExpenseModel model)
        {
            model.Item = new List<ItemModel>();
            model.Expense = new ExpenseModel();
            if(model.Item.Count > 0)
            {
                //do nothing
            }
            
            return PartialView(model);
        }
    }
}

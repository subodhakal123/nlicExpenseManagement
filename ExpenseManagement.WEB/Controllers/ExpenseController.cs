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

        public async Task<ActionResult> AddEditExpense(ItemExpenseModel model)
        {
            model.Item = new List<ItemModel>();
            ExpenseModel expenseModel = new ExpenseModel();
            
            if(model.ExpenseId > 0)
            {
                int expId = model.ExpenseId;
                var client = new RestClient();
                var request = new RestRequest();
                request.Method = Method.Post;
                request.Resource = "https://localhost:7250/api/Expense/GetExpenseById";
                request.AddQueryParameter("ExpenseId",expId);
                //request.AddJsonBody(expId);
                ItemExpenseModel response = await client.PostAsync<ItemExpenseModel>(request);
                model = response;

            }
            
            return PartialView(model);

        }
    }
}

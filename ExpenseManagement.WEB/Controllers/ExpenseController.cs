using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using ExpenseManagement.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace ExpenseManagement.Web.Controllers
{
    public class ExpenseController : Controller
    {
        public WebApiService _ws;
        RestClient client;
        public ExpenseController()
        {
            client = new RestClient();
            _ws = new WebApiService(null);

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AddEditExpense(ItemExpenseModel model)
        {
            model.Item = new List<ItemModel>();


            var abc = User.Identity.Equals("AccessToken");
            var cde = User.Claims;
            
            
            if(model.ExpenseId > 0)
            {
                var request = new RestRequest();
                request.Method = Method.Post;
                request.Resource = "https://localhost:7250/api/Expense/GetExpenseById";
                request.AddQueryParameter("ExpenseId", model.ExpenseId);
                RestResponse response = _ws.GetResponse(request);
                model = JsonConvert.DeserializeObject< ItemExpenseModel>( response.Content);

            }
            
            return PartialView(model);

        }
    }
}

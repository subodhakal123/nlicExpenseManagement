using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using ExpenseManagement.Web.Helper;
using ExpenseManagement.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;

namespace ExpenseManagement.Web.Controllers
{
    [Authorize]
    public class ExpenseController : BaseController
    {
        public WebApiService _ws;
        RestClient client;
        string WebApiUri;
        int userId;
        public ExpenseController()
        {
            client = new RestClient();
            _ws = new WebApiService(null);
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            WebApiUri = configuration.GetValue<string>("WebApiUrl");
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AddEditExpense(ItemExpenseModel model)
        {
            model.Item = new List<ItemModel>();
            var model2 = new ItemExpenseViewModel();
            var model3 = new GetExpenseById();
            model3.ExpenseId = model.ExpenseId;

            if (model.ExpenseId > 0)
            {
                var identity = HttpContext.User as ClaimsPrincipal;
                model3.UserId = Int16.Parse(identity.Claims.First(c => c.Type == "UserId").Value);
                _ws = new WebApiService(identity);

                var request = new RestRequest();
                request.Method = Method.Post;
                request.Resource = WebApiUri + "/Expense/GetExpenseById";
                request.AddJsonBody(model3);
                RestResponse response = _ws.GetResponse(request);
                model2 = JsonConvert.DeserializeObject<ItemExpenseViewModel>( response.Content);

            }
            
            return PartialView(model2);

        }
    }
}

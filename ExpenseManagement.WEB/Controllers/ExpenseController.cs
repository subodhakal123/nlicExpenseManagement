﻿using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using ExpenseManagement.Web.Helper;
using ExpenseManagement.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace ExpenseManagement.Web.Controllers
{
    public class ExpenseController : BaseController
    {
        public WebApiService _ws;
        RestClient client;
        string WebApiUri;
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
            var cde = WebApiUri;

            var abc = User.Identity.Equals("AccessToken");
            var efg = User.Claims;
            
            
            if(model.ExpenseId > 0)
            {
                var request = new RestRequest();
                request.Method = Method.Post;
                request.Resource = WebApiUri + "/Expense/GetExpenseById";
                request.AddQueryParameter("ExpenseId", model.ExpenseId);
                RestResponse response = _ws.GetResponse(request);
                model = JsonConvert.DeserializeObject< ItemExpenseModel>( response.Content);

            }
            
            return PartialView(model);

        }
    }
}

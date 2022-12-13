using ExpenseManagement.BLL.Expense;
using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ExpenseManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
		private readonly IExpenseService _es;
		public ExpenseController(IExpenseService userService)
		{
			_es = userService;
		}
		[HttpPost("GetAllExpense")]
		public ArrayList GetAllExpense(FilterSortModel fm)
		{
			ArrayList data = _es.GetAllExpense(fm);
			return data;
		}

		[HttpPost("GetExpenseById")]
		public List<ItemModel> GetExpenseById(ExpenseModel model)
		{
			List<ItemModel> dm = _es.GetExpenseById(model.ExpenseId);
			return dm;
		}

		[HttpPost("SaveExpense")]
		public string SaveExpense(ItemExpenseModel model)
		{
			string msg = _es.SaveExpense(model);
			return msg;
		}

		[HttpGet("DeleteExpense")]
		public string DeleteExpense(int ExpenseId)
		{
			string msg = _es.DeleteExpense(ExpenseId);
			return msg;
		}
	}
}

using ExpenseManagement.BLL.Expense;
using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using Microsoft.AspNetCore.Authorization;
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
		public ItemExpenseModel GetExpenseById(int ExpenseId)
		{
			ItemExpenseModel dm = _es.GetExpenseById(ExpenseId);
			return dm;
		}

		[HttpPost("SaveExpense")]
		public SaveExpense SaveExpense(ItemExpenseModel model)
		{
			SaveExpense obj =  _es.SaveExpense(model);
			return obj;
		}

		[HttpGet("DeleteExpense")]
		public string DeleteExpense(int ExpenseId)
		{
			string msg = _es.DeleteExpense(ExpenseId);
			return msg;
		}
		[HttpPost("ApproveExpense")]
		public string ApproveExpense(ApproveRequestModel model)
		{
			return _es.ApproveExpense(model);
		}
	}
}

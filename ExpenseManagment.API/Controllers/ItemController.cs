using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using ItemManagement.BLL.Item;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ExpenseManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
		private readonly IItemService _is;
		public ItemController(IItemService userService)
		{
			_is = userService;
		}
		[HttpPost("GetAllItem")]
		public ArrayList GetAllItem(FilterSortModel fm)
		{
			ArrayList data = _is.GetAllItem(fm);
			return data;
		}

		[HttpPost("GetItemById")]
		public ItemModel GetItemById(ItemModel model)
		{
			var dm = _is.GetItemById(model.ItemId);
			return dm;
		}

		[HttpPost("SaveItem")]
		public string SaveItem(ItemModel model)
		{
			string msg = _is.SaveItem(model);
			return msg;
		}

		[HttpGet("DeleteItem")]
		public string DeleteItem(int ItemId)
		{
			string msg = _is.DeleteItem(ItemId);
			return msg;
		}
	}
}

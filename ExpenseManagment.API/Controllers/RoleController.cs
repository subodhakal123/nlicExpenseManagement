using ExpenseManagement.BLL.Account;
using ExpenseManagement.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
	{
        private readonly IRoleService _us;
        public RoleController(IRoleService userService)
        {
            _us = userService;
        }
		[HttpPost("GetAllRoles")]
		public ArrayList GetAllRoles(FilterSortModel fm)
		{
			ArrayList data = _us.GetAllRoles(fm);
			return data;
		}

		[HttpPost("GetRoleById")]
		public RolesModel GetRoleById(RolesModel model)
		{
			var dm = _us.GetRoleById(model.RoleId);
			return dm;
		}

		[HttpPost("SaveRole")]
		public string SaveRole(RolesModel model)
		{
			string msg = _us.SaveRole(model);
			return msg;
		}

		[HttpGet("DeleteRole")]
		public string DeleteRole(int RoleId, string UserName)
		{
			string msg = _us.DeleteRole(RoleId, UserName);
			return msg;
		}

		[HttpGet("GetRoleDetailById")]
		public List<RolesModel> GetRoleDetailById(int roleId)
		{
			var list = _us.GetRoleDetailById(roleId);
			return list;
		}

		[HttpGet("GetRoles")]
		public List<RolesModel> GetRoles()
		{
			var list = _us.GetRoles();
			return list;
		}

		[HttpGet("GetNavigationPages")]
		public List<UserRolesNagivationMenu> GetNavigationPages()
		{
			var list = _us.GetNavigationPages();
			return list;
		}
		[HttpGet("GetRolePages")]
		public List<UserRolesNagivationMenu> GetRolePages(int RoleId)
		{
			var list = new List<UserRolesNagivationMenu>();
			list = _us.GetRolePages(RoleId);
			return list;
		}

		[HttpPost("SaveRolePage")]
		public string SaveRolePage(RolePageSaveModel model)
		{
			string msg = _us.SaveRolePages(model);
			return msg;
		}
	}
}

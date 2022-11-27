using ExpenseManagement.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Settings.Role
{
    public interface IRoleService
    {
		ArrayList GetAllRoles(FilterSortModel model);
		RolesModel GetRoleById(int roleId);
		string SaveRole(RolesModel model);
		string DeleteRole(int RoleId, string UserName);
		List<RolesModel> GetRoleDetailById(int roleId);
		List<RolesModel> GetRoles();
		List<UserRolesNagivationMenu> GetNavigationPages();
		List<UserRolesNagivationMenu> GetRolePages(int roleId);
		string SaveRolePages(RolePageSaveModel model);
	}
}

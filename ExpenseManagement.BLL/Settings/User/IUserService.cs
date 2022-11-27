using ExpenseManagement.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Settings.User
{
    public interface IUserService
    {
		ArrayList GetAllUsers(FilterSortModel model);
		UserViewModel GetUserById(int? UserId);
		bool ValidateUserParameter(string ParameterName, string ParameterVal, int? UserId);
		string SaveUser(UserViewModel model);
		string DeleteUser(int UserId, string LoginUserName);
		List<UserViewModel> GetUserDetailById(int UserId);

		string SaveUserName(UserViewModel model);
		List<UserViewModel> GetUserNameById(int UserId);

		string SaveUserRoles(UserRoleSaveModel model);

		List<UserRolesGetViewModel> GetUserRoles(int UserId);

		List<UserRolesNagivationMenu> GetNagivationMenuById(int UserId, int RoleId);

		List<UserRolesNavAuthority> GetRoleMenuAuthority(int MenuId, int RoleId, int UserId);

		string SaveUserMenuAuthority(UserNavAuthSaveViewModel model);
		UserLimitAuthorityModulesExistModel GetUserAuthLimitMenu(int UserId);
		UserLimitAuthorityModel GetUserAuthorityLimit(int UserId);
		string UserAuthLimitInsDel(UserLimitAuthorityModel model);
	}
}

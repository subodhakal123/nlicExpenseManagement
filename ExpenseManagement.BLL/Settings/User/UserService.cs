using Dapper;
using ExpenseManagement.BLL.GridFilterSort;
using ExpenseManagement.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Settings.User
{
    public class UserService : IUserService
    {
        private IDbConnection db;
        public UserService(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public string DeleteUser(int UserId, string LoginUserName)
        {
            var msg = db.Query<string>("[USRS].[usp_User_Del]", new { UserId = UserId, LoginUserName = LoginUserName }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return msg;
        }

        public ArrayList GetAllUsers(FilterSortModel model)
        {
            SortModel sort = new SortModel();
            if (model.sort.Count() > 0)
            {
                sort = model.sort.FirstOrDefault();
            }
            else
            {
                sort.Dir = "";
                sort.Field = "";
            }
            var whereCondition = FilterSortHelper.BuildWhereClause(model.Filter);
            var sortCondition = FilterSortHelper.OrderField(sort.Field);
            var list = new List<UserViewModel>();
            var dp = new DynamicParameters();
            dp.Add("@TotalRecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //dp.Add("@UserId", model.UserId);
            dp.Add("@OffsetRows", model.skip);
            dp.Add("@FetchRows", model.take);
            dp.Add("@WhereClause", whereCondition);
            dp.Add("@SortOrder", sort.Dir);
            dp.Add("@SortField", sortCondition);
            dp.Add("@CultureCd", model.CultureCode);
            list = db.Query<UserViewModel>("[USRS].[usp_User_AllGet]", dp, commandType: CommandType.StoredProcedure).ToList();
            ArrayList al = new ArrayList();
            al.Add(list);
            al.Add(dp.Get<int>("TotalRecordCount"));
            return al;
        }

        public UserViewModel GetUserById(int? UserId)
        {
            UserViewModel model = new UserViewModel();
            model = db.Query<UserViewModel>("[USRS].[usp_User_Get]", new { UserId = UserId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return model;
        }

        public List<UserViewModel> GetUserDetailById(int UserId)
        {
            //var list = new List<UserViewModel>();
            //list = db.Query<UserViewModel>("[dbo].[SpUserDetailGet]", new { UserId = UserId }, commandType: CommandType.StoredProcedure).ToList();
            return null;
        }

        public List<UserViewModel> GetUserNameById(int UserId)
        {
            var list = new List<UserViewModel>();
            list = db.Query<UserViewModel>("[USRS].[usp_UserName_Get]", new { UserId = UserId }, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        public string SaveUser(UserViewModel model)
        {
            string strReturnMsg = "";
            try
            {
                if (String.IsNullOrEmpty(model.MiddleName))
                {
                    model.FullName = model.FirstName.Trim() + ' ' + model.LastName.Trim();
                }
                else
                {
                    model.FullName = model.FirstName.Trim() + ' ' + model.MiddleName.Trim() + ' ' + model.LastName.Trim();
                }
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@UserId", model.UserId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                dp.Add("@FirstName", String.IsNullOrEmpty(model.FirstName) ? model.FirstName : model.FirstName.Trim());
                dp.Add("@MiddleName", String.IsNullOrEmpty(model.MiddleName) ? model.MiddleName : model.MiddleName.Trim());
                dp.Add("@LastName", String.IsNullOrEmpty(model.LastName) ? model.LastName : model.LastName.Trim());
                dp.Add("@FullName", model.FullName.Trim());
                dp.Add("@Gender", String.IsNullOrEmpty(model.Gender) ? model.Gender : model.Gender.Trim());
                dp.Add("@StaffId", model.StaffId);
                dp.Add("@DepartmentId", model.DepartmentId);
                dp.Add("@Mobile", String.IsNullOrEmpty(model.Mobile) ? model.Mobile : model.Mobile.Trim());
                dp.Add("@Email", String.IsNullOrEmpty(model.Email) ? model.Email : model.Email.Trim());
                dp.Add("@PostId", model.PostId);
                dp.Add("@JoinDate", model.JoinDate);
                dp.Add("@BrCode", String.IsNullOrEmpty(model.BrCode) ? model.BrCode : model.BrCode.Trim());
                dp.Add("@IsLocked", String.IsNullOrEmpty(model.IsLocked) ? model.IsLocked : model.IsLocked.Trim());
                dp.Add("@IsHead", String.IsNullOrEmpty(model.IsHead) ? model.IsHead : model.IsHead.Trim());
                dp.Add("@IsBlocked", String.IsNullOrEmpty(model.IsBlocked) ? model.IsBlocked : model.IsBlocked.Trim());
                dp.Add("@IsOnForcedLeave", String.IsNullOrEmpty(model.IsOnForcedLeave) ? model.IsOnForcedLeave : model.IsOnForcedLeave.Trim());
                dp.Add("@CreatedBy", String.IsNullOrEmpty(model.LoginUserName) ? model.LoginUserName : model.LoginUserName.Trim());
                var affectedRows = db.Query("[USRS].[usp_User_InsUpd]", dp, commandType: CommandType.StoredProcedure);
                strReturnMsg = dp.Get<string>("retMsg");
                model.UserId = dp.Get<int>("UserId");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }

        public string SaveUserName(UserViewModel model)
        {
            string strReturnMsg = "";
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@UserId", model.UserId);
                dp.Add("@UserName", String.IsNullOrEmpty(model.UserName) ? model.UserName : model.UserName.Trim());

                var affectedRows = db.Query("[USRS].[usp_User_AddInsUpd]", dp, commandType: CommandType.StoredProcedure);
                strReturnMsg = dp.Get<string>("retMsg");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }

        public bool ValidateUserParameter(string ParameterName, string ParameterVal, int? UserId)
        {
            bool doExist = true;
            string value;
            if (UserId > 0)
            {
                var sql1 = "select " + ParameterName + " as value from USRS.WebUsers where " + ParameterName + " = @params and UserId not in (@UserId) and IsDeleted <> 1 ";
                value = db.Query<string>(sql1.ToString(), new { @params = ParameterVal, @UserId = UserId }).SingleOrDefault();
            }
            else
            {
                var sql2 = "select " + ParameterName + " as value from USRS.WebUsers where " + ParameterName + " = @params";
                value = db.Query<string>(sql2.ToString(), new { @params = ParameterVal }).SingleOrDefault();
            }

            if (value == null)
            {
                doExist = false;
            }
            return doExist;
        }

        public string SaveUserRoles(UserRoleSaveModel model)
        {
            string strReturnMsg = "";
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@UserId", model.UserId);
                dp.Add("@RoleList", String.IsNullOrEmpty(model.RolesList) ? model.RolesList : model.RolesList.Trim());
                dp.Add("@LoginUser", String.IsNullOrEmpty(model.LoginUser) ? model.LoginUser : model.LoginUser.Trim());

                var affectedRows = db.Query("[USRS].[usp_UserRole_InsDel]", dp, commandType: CommandType.StoredProcedure);
                strReturnMsg = dp.Get<string>("retMsg");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }

        public List<UserRolesGetViewModel> GetUserRoles(int UserId)
        {
            var list = new List<UserRolesGetViewModel>();
            list = db.Query<UserRolesGetViewModel>("[USRS].[usp_UserRole_AllGet]", new { UserId = UserId }, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }
        public string SaveUserMenuAuthority(UserNavAuthSaveViewModel model)
        {
            string strReturnMsg = "";
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@UserId", model.UserId);
                dp.Add("@MenuAuthorityIdRemList", String.IsNullOrEmpty(model.MenuAuthorityIdRemList) ? model.MenuAuthorityIdRemList : model.MenuAuthorityIdRemList.Trim());
                dp.Add("@MenuAuthorityIdAddList", String.IsNullOrEmpty(model.MenuAuthorityIdAddList) ? model.MenuAuthorityIdAddList : model.MenuAuthorityIdAddList.Trim());
                var affectedRows = db.Query("[USRS].[usp_UserMenuAuthority_Upd]", dp, commandType: CommandType.StoredProcedure);
                strReturnMsg = dp.Get<string>("retMsg");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }

        public List<UserRolesNagivationMenu> GetNagivationMenuById(int UserId, int RoleId)
        {
            var list = new List<UserRolesNagivationMenu>();
            list = db.Query<UserRolesNagivationMenu>("[USRS].[usp_UserRoleNavMenu_Get]", new { UserId = UserId, RoleId = RoleId }, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        public List<UserRolesNavAuthority> GetRoleMenuAuthority(int MenuId, int RoleId, int UserId)
        {
            var list = new List<UserRolesNavAuthority>();
            list = db.Query<UserRolesNavAuthority>("[USRS].[usp_UserNavAuthority_Get]", new { MenuId = MenuId, RoleId = RoleId, UserId = UserId }, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        public UserLimitAuthorityModulesExistModel GetUserAuthLimitMenu(int UserId)
        {
            UserLimitAuthorityModulesExistModel data = new UserLimitAuthorityModulesExistModel();
            db.Open();
            data = db.Query<UserLimitAuthorityModulesExistModel>("[USRS].[usp_UserNavMenu_AuthAccess_Get]", new { @UserId = UserId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            db.Close();
            return data;
        }

        public UserLimitAuthorityModel GetUserAuthorityLimit(int UserId)
        {
            UserLimitAuthorityModel data = new UserLimitAuthorityModel();
            db.Open();
            data = db.Query<UserLimitAuthorityModel>("[USRS].[usp_UserNavMenu_AuthLimit_Get]", new { @UserId = UserId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            db.Close();
            data = data ?? new UserLimitAuthorityModel();
            return data;
        }

        public string UserAuthLimitInsDel(UserLimitAuthorityModel model)
        {
            string strReturnMsg = "";
            model.UndMedicalLimit = model.UndMedicalLimit ?? 0;
            model.UndNonMedicalLimit = model.UndNonMedicalLimit ?? 0;
            model.MaturityLimit = model.MaturityLimit ?? 0;
            model.LoanLimit = model.LoanLimit ?? 0;
            model.SurrenderLimit = model.SurrenderLimit ?? 0;
            model.SurvivalLimit = model.SurvivalLimit ?? 0;
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@UserId", model.UserId);
                dp.Add("@UndMedicalLimit", model.UndMedicalLimit);
                dp.Add("@UndNonMedicalLimit", model.UndNonMedicalLimit);
                dp.Add("@MaturityLimit", model.MaturityLimit);
                dp.Add("@LoanLimit", model.LoanLimit);
                dp.Add("@SurrenderLimit", model.SurrenderLimit);
                dp.Add("@SurvivalLimit", model.SurvivalLimit);
                dp.Add("@UserAuthorityTo", model.UserAuthorityTo);
                var affectedRows = db.Query("[USRS].[usp_UserAuthLimit_InsDel]", dp, commandType: CommandType.StoredProcedure);
                strReturnMsg = dp.Get<string>("retMsg");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }


    }
}

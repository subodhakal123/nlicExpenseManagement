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

namespace ExpenseManagement.BLL.Account
{
    public class RoleService : IRoleService
	{
        private IDbConnection db;
        public RoleService(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public string DeleteRole(int RoleId, string UserName)
        {
            var msg = db.Query<string>("[USRS].[usp_Role_Del]", new { RoleId = RoleId, UserName = UserName }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return msg;
        }

        public ArrayList GetAllRoles(FilterSortModel model)
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
            var list = new List<RolesModel>();
            var dp = new DynamicParameters();
            dp.Add("@TotalRecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dp.Add("@UserId", model.UserId);
            dp.Add("@OffsetRows", model.skip);
            dp.Add("@FetchRows", model.take);
            dp.Add("@WhereClause", whereCondition);
            dp.Add("@SortOrder", sort.Dir);
            dp.Add("@SortField", sortCondition);
            dp.Add("@CultureCd", model.CultureCode);
            list = db.Query<RolesModel>("[USRS].[usp_Role_AllGet]", dp, commandType: CommandType.StoredProcedure).ToList();
            ArrayList al = new ArrayList();
            al.Add(list);
            al.Add(dp.Get<int>("TotalRecordCount"));
            return al;
        }

        public List<UserRolesNagivationMenu> GetNavigationPages()
        {
            var list = new List<UserRolesNagivationMenu>();
            var dp = new DynamicParameters();
            dp.Add("@RoleId", "");
            list = db.Query<UserRolesNagivationMenu>("[USRS].[usp_NavigationPages_Get]", new {RoleId = ""}, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        public RolesModel GetRoleById(int roleId)
        {
            RolesModel model = new RolesModel();
            model = db.Query<RolesModel>("[USRS].[usp_Role_Get]", new { RoleId = roleId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return model;
        }

        public List<RolesModel> GetRoleDetailById(int roleId)
        {
            //var list = new List<RolesModel>();
            //list = db.Query<RolesModel>("[dbo].[SpRoleDetailGet]", new { RoleId = roleId }, commandType: CommandType.StoredProcedure).ToList();
            return null;
        }

        public List<UserRolesNagivationMenu> GetRolePages(int roleId)
        {
            var list = new List<UserRolesNagivationMenu>();
            var dp = new DynamicParameters();
            dp.Add("@RoleId", roleId);
            list = db.Query<UserRolesNagivationMenu>("[USRS].[usp_NavigationPages_Get]", new { RoleId = roleId }, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        public List<RolesModel> GetRoles()
        {
            var list = new List<RolesModel>();
            list = db.Query<RolesModel>("[USRS].[usp_Role_Get]", new { RoleId = "" }, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        public string SaveRole(RolesModel model)
        {
            string strReturnMsg = "";
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@RoleId", model.RoleId);
                dp.Add("@RoleName", model.RoleName);
                dp.Add("@RoleCode", model.RoleCode);
                dp.Add("@IsActive", model.IsActive);
                dp.Add("@CreatedBy", model.UserName);
                var affectedRows = db.Query("[USRS].[usp_Role_InsUpd]", dp, commandType: CommandType.StoredProcedure);
                strReturnMsg = dp.Get<string>("retMsg");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }

        public string SaveRolePages(RolePageSaveModel model)
        {
            string strReturnMsg = "";
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@RoleId", model.RoleId);
                dp.Add("@PageList", String.IsNullOrEmpty(model.PageList) ? model.PageList : model.PageList.Trim());
                dp.Add("@UserName", String.IsNullOrEmpty(model.LoginUserName) ? model.LoginUserName : model.LoginUserName.Trim());

                var affectedRows = db.Query("[USRS].[usp_RolePage_InsDel]", dp, commandType: CommandType.StoredProcedure);
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

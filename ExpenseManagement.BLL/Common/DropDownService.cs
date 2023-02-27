using Dapper;
using ExpenseManagement.Model.Expense;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpenseManagement.Model.Common.DropDownModel;

namespace ExpenseManagement.BLL.Common
{
    public class DropDownService: IDropDownService
    {
		private IDbConnection db;
		public DropDownService(IConfiguration configuration)
		{
			this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
		}

		public List<DropDown> GetDropDowns(DropDownCallParameter callParameter)
		{
			List<DropDown> ddList = new List<DropDown>();
			try
			{
				var dp = new DynamicParameters();
				dp.Add("@Mode", callParameter.mode);
				dp.Add("@Condition1", callParameter.condition1);
				dp.Add("@Condition2", callParameter.condition2);
				ddList.AddRange(db.Query<DropDown>("[MST].[usp_Dropdown_ListGet]", dp, commandType: CommandType.StoredProcedure).ToList());
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error:" + ex.Message);
			}
			return ddList;
		}

		public List<DropDown> GetForwardListDropDowns(GetExpenseById callParameter)
        {
			List<DropDown> ddList = new List<DropDown>();
			try
			{
				var dp = new DynamicParameters();
				dp.Add("@ExpenseId", callParameter.ExpenseId);
				dp.Add("@UserId", callParameter.UserId);
				ddList.AddRange(db.Query<DropDown>("[MST].[usp_ForwardList_Dropdown_ListGet]", dp, commandType: CommandType.StoredProcedure).ToList());
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error:" + ex.Message);
			}
			return ddList;
		}
	}
}

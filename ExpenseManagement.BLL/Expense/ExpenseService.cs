using Dapper;
using ExpenseManagement.BLL.GridFilterSort;
using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Expense
{
    public class ExpenseService : IExpenseService
    {
        private IDbConnection db;
        public ExpenseService(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public string DeleteExpense(int ExpenseId)
        {
            var msg = db.Query<string>("[EXP].[usp_Expense_Del]", new { expenseId = ExpenseId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return msg;
        }

        public ArrayList GetAllExpense(FilterSortModel model)
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

            var list = new List<ExpenseModel>();
            var dp = new DynamicParameters();
            dp.Add("@TotalRecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dp.Add("@UserId", model.UserId);
            dp.Add("@OffsetRows", model.skip);
            dp.Add("@FetchRows", model.take);
            dp.Add("@WhereClause", whereCondition);
            dp.Add("@SortOrder", sort.Dir);
            dp.Add("@SortField", sortCondition);
            dp.Add("@CultureCd", model.CultureCode);
            list = db.Query<ExpenseModel>("[EXP].[usp_Expense_AllGet]", dp, commandType: CommandType.StoredProcedure).ToList();
            ArrayList al = new ArrayList();
            al.Add(list);
            al.Add(dp.Get<int>("TotalRecordCount"));
            return al;
        }

        public ExpenseModel GetExpenseById(int ExpenseId)
        {
            ExpenseModel model = new ExpenseModel();
            model = db.Query<ExpenseModel>("[EXP].[usp_Expense_Get]", new { expenseId = ExpenseId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return model;
        }

        public string SaveExpense(ExpenseModel model)
        {
            string strReturnMsg = "";
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@Expense_ID", model.ExpenseId);
                dp.Add("@Expense_Date", model.ExpenseDate);
                dp.Add("@Branch_Name", model.BranchName);
                dp.Add("@Expense_Type", model.ExpenseType);
                dp.Add("@Expense_SubType", model.ExpenseSubType);
                dp.Add("@Amount", model.Amount);
                dp.Add("@Total_Amount", model.TotalAmount);
                dp.Add("@Is_Recommended", model.IsRecommended);
                dp.Add("@Department_Name", model.DepartmentName);
                dp.Add("@Comment", model.Comment);
                var affectedRows = db.Query("[EXP].[usp_Expense_InsUpd]", dp, commandType: CommandType.StoredProcedure);
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

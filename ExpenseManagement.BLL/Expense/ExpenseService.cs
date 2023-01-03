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
using System.Reflection;
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

        public List<ItemModel> GetExpenseById(int ExpenseId)
        {
            var list = new List<ItemModel>();
            try
            {
                list = db.Query<ItemModel>("[EXP].[usp_Expense_Get]", new { expenseId = ExpenseId }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            
            return list;
        }

        public SaveExpense SaveExpense(ItemExpenseModel model)
        {
            SaveExpense saveExpense = new SaveExpense();
            try
            {
                DataTable udtExpenseDetail = ListToDataTable(model.Item);
                var parameter = new DynamicParameters();
                parameter.Add("@ExpenseId", model.ExpenseId);
                parameter.Add("@udtExpenseDetail", udtExpenseDetail.AsTableValuedParameter("[dbo].[udtExpenseDetail1]"));
                //parameter.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                //this.db.Execute("[EXP].[usp_Expense_InsUpd]", parameter, commandType: CommandType.StoredProcedure);
                //strReturnMsg = parameter.Get<string>("retMsg");

                saveExpense =  db.Query<SaveExpense>("[EXP].[usp_Expense_InsUpd]", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                //saveExpense = obj;
            }
            catch (Exception ex)
            {
                saveExpense.retMsg = "Error: " + ex.Message.ToString();
            }
            return saveExpense;
        }

        public string ApproveExpense(ApproveRequestModel model)
        {
            string strReturnMsg = "";
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ExpenseId", model.ExpenseId);
                parameter.Add("@Username", model.Username);
                parameter.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                this.db.Execute("[EXP].[usp_ExpenseApproval_InsUpd]", parameter, commandType: CommandType.StoredProcedure);
                strReturnMsg = parameter.Get<string>("retMsg");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }

        public DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable("dt");

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            if (items != null)
            {
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            }
            dataTable.AcceptChanges();
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}

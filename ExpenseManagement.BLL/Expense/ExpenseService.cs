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
            ArrayList al = new ArrayList();
            try { 
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
                
                al.Add(list);
                al.Add(dp.Get<int>("TotalRecordCount"));
            }
            catch(Exception ex)
            {

            }
            return al;
        }

        public ItemExpenseViewModel GetExpenseById(GetExpenseById model)
        {
            List<ItemViewModel> list = new List<ItemViewModel>();
            ItemExpenseViewModel list2 = new ItemExpenseViewModel();
            list2.Item = new List<ItemModel>();
            try
            {
                list = db.Query<ItemViewModel>("[EXP].[usp_Expense_Get]", new { expenseId = model.ExpenseId,userId = model.UserId }, commandType: CommandType.StoredProcedure).ToList();
                list2.ExpenseId = list[0].ExpenseId;
                list2.ExpenseTitle = list[0].ExpenseTitle;
                list2.ExpenseBy = list[0].ExpenseBy;
                list2.BrCode = list[0].BrCode;
                list2.BranchName = list[0].BranchName;
                list2.AppliedBy = list[0].AppliedBy;
                list2.Status = list[0].Status;
                list2.ExpenseDate = list[0].ExpenseDate;
                list2.IsRecommended = list[0].IsRecommended;
                list2.Recommender = list[0].Recommender;
                list2.DepartmentId = list[0].DepartmentName;
                list2.Comment = list[0].Comment;
                list2.IsApproved = list[0].IsApproved;
                list2.ApprovedBy = list[0].ApprovedBy;
                list2.ApprovedDate = list[0].ApprovedDate;
                list2.ApproverName = list[0].ApproverName;
                list2.IsAuthorisedToApprove = list[0].IsAuthorisedToApprove;
                list2.TotalAmount = list[0].TotalAmount;

                foreach(ItemViewModel item in list)
                {
                    ItemModel model1 = new ItemModel();
                    model1.ExpenseId = item.ExpenseId;
                    model1.ItemId = item.ItemId;
                    model1.ItemName = item.ItemName;
                    model1.ExpenseType = item.ExpenseType;
                    model1.ItemQuantity = item.ItemQuantity;
                    model1.ItemAmount = item.ItemAmount;
                    model1.ItemPrice = item.ItemPrice;
                    list2.Item.Add(model1);
                }
            }
            catch (Exception ex)
            {
            
            }

            //var list = new List<ItemModel>();
            //try
            //{
            //    list = db.Query<ItemModel>("[EXP].[usp_Expense_Get]", new { expenseId = ExpenseId }, commandType: CommandType.StoredProcedure).ToList();
            //}
            //catch (Exception ex)
            //{
            //
            //}

            return list2;
        }

        public SaveExpense SaveExpense(ItemExpenseModel model)
        {
            SaveExpense saveExpense = new SaveExpense();
            try
            {

                DataTable udtExpenseDetail = ListToDataTable(model.Item);
                var parameter = new DynamicParameters();
                parameter.Add("@ExpenseId",model.ExpenseId,direction: ParameterDirection.InputOutput);
                parameter.Add("@ExpenseTitle", model.ExpenseTitle);
                parameter.Add("@ExpenseBy", model.ExpenseBy);
                parameter.Add("@AppliedBy", model.AppliedBy);
                parameter.Add("@BranchName", model.BranchName);
                parameter.Add("@Status", model.Status);
                parameter.Add("@udtExpenseDetail", udtExpenseDetail.AsTableValuedParameter("[dbo].[udtExpenseDetail]"));
                parameter.Add("@IsRecommended", model.IsRecommended);
                parameter.Add("@Department", model.DepartmentId);
                parameter.Add("@Recommender", model.Recommender);
                parameter.Add("@Comment", model.Comment);
                parameter.Add("@TotalAmount", model.TotalAmount);
                parameter.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                db.Execute("[EXP].[usp_Expense_InsUpd]", parameter, commandType: CommandType.StoredProcedure);
                saveExpense.retMsg = parameter.Get<string?>("retMsg");
                saveExpense.ExpenseId = parameter.Get<int?>("ExpenseId");
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
                parameter.Add("@UserId", model.UserId);
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

        public string CancelExpenseApproval(ApproveRequestModel model)
        {
            string strReturnMsg = "";
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ExpenseId", model.ExpenseId);
                parameter.Add("@UserId", model.UserId);
                parameter.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                this.db.Execute("[EXP].[usp_Cancel_ExpenseApproval]", parameter, commandType: CommandType.StoredProcedure);
                strReturnMsg = parameter.Get<string>("retMsg");
            }
            catch (Exception ex)
            {
                strReturnMsg = "Error: " + ex.Message.ToString();
            }
            return strReturnMsg;
        }

        public string ApprovalRequest(RequestApproval model)
        {
            string strReturnMsg = "";
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ExpenseId", model.ExpenseId);
                parameter.Add("@UserId", model.userId);
                parameter.Add("@ForwardTo", model.ForwardTo);
                parameter.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                this.db.Execute("[EXP].[usp_RequestExpenseApproval]", parameter, commandType: CommandType.StoredProcedure);
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

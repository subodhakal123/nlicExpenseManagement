using Dapper;
using ExpenseManagement.BLL.GridFilterSort;
using ExpenseManagement.Model;
using ExpenseManagement.Model.Expense;
using ItemManagement.BLL.Item;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManagement.BLL.Expense
{
    public class ItemService: IItemService
    {
        private IDbConnection db;
        public ItemService(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public string DeleteItem(int ItemId)
        {
            var msg = db.Query<string>("[EXP].[usp_Item_Del]", new { itemId = ItemId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return msg;
        }

        public ArrayList GetAllItem(FilterSortModel model)
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
            var list = new List<ItemModel>();
            var dp = new DynamicParameters();
            dp.Add("@TotalRecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dp.Add("@UserId", model.UserId);
            dp.Add("@OffsetRows", model.skip);
            dp.Add("@FetchRows", model.take);
            dp.Add("@WhereClause", whereCondition);
            dp.Add("@SortOrder", sort.Dir);
            dp.Add("@SortField", sortCondition);
            dp.Add("@CultureCd", model.CultureCode);
            list = db.Query<ItemModel>("[USRS].[usp_Item_AllGet]", dp, commandType: CommandType.StoredProcedure).ToList();
            ArrayList al = new ArrayList();
            al.Add(list);
            al.Add(dp.Get<int>("TotalRecordCount"));
            return al;
        }

        public ItemModel GetItemById(int ItemId)
        {
            ItemModel model = new ItemModel();
            model = db.Query<ItemModel>("[EXP].[usp_Item_Get]", new { itemId = ItemId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return model;
        }

        public string SaveItem(ItemModel model)
        {
            string strReturnMsg = "";
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@retMsg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                dp.Add("@Item_Id", model.ItemId);
                dp.Add("@Expense_ID", model.ExpenseId);
                dp.Add("@Item_Name", model.ItemName);
                dp.Add("@Item_Type", model.ItemType);
                dp.Add("@Item_Desc", model.ItemDesc);
                dp.Add("@Item_Price", model.ItemPrice);
                dp.Add("@Item_Quantity", model.ItemQuantity);
                dp.Add("@Item_Amount", model.ItemAmount);
                var affectedRows = db.Query("[EXP].[usp_Item_InsUpd]", dp, commandType: CommandType.StoredProcedure);
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

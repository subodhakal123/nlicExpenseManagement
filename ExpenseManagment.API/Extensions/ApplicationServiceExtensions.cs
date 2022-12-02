using ExpenseManagement.BLL.Account;
using ExpenseManagement.BLL.Common;
using ExpenseManagement.BLL.Expense;
using ItemManagement.BLL.Expense;
using ItemManagement.BLL.Item;

namespace ExpenseManagment.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {

            Services.AddScoped<IAccountService, AccountService>();
            Services.AddScoped<IRoleService, RoleService>();
            Services.AddScoped<IExpenseService, ExpenseService>();
            Services.AddScoped<IItemService, ItemService>();
            Services.AddScoped<IDropDownService, DropDownService>();
            return Services;
        }
    }
}

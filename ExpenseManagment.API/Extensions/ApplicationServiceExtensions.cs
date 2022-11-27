using ExpenseManagement.BLL.Account;

namespace ExpenseManagment.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {

            Services.AddScoped<IRoleService, RoleService>();
            return Services;
        }
    }
}

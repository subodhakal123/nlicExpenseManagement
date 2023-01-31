using ExpenseManagement.Model;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;

namespace ExpenseManagement.Web.Helper
{
    public abstract class BaseView<TModel> : RazorPage<TModel>
    {
        protected AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }
        public string WebApiUri
        {
            get
            {
                var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();
                return configuration.GetValue<string>("WebApiUrl");
            }
        }
    }

    public abstract class BaseView : BaseView<dynamic>
    {
    }
}

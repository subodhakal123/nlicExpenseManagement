using Microsoft.AspNetCore.Mvc.Razor;

namespace ExpenseManagement.Web.Helper
{
    public abstract class BaseView<TModel> : RazorPage<TModel>
    {
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

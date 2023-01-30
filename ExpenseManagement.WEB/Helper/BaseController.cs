using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Web.Helper
{
    public abstract class BaseController: Controller
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
}

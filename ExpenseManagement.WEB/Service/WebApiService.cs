using ExpenseManagement.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using RestSharp;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace ExpenseManagement.Web.Service
{
    [Authorize]
    public class WebApiService: Controller
    {
        RestClient client;
        private readonly ClaimsPrincipal identity;
        string WebApiUri;
        //private readonly IClaimsService _claimService;
        public WebApiService(IPrincipal principal)
        {
            identity = principal as ClaimsPrincipal;
            client = new RestClient();

            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            WebApiUri = configuration.GetValue<string>("WebApiUrl");
        }

        public RestResponse GetResponse(RestRequest request)
        {
            if (identity.FindFirst("AccessToken") != null)
            {
            
                var AccessToken = identity.FindFirst("AccessToken").Value;
                request.AddParameter("Authorization", string.Format("Bearer {0}", AccessToken), ParameterType.HttpHeader);
              RestResponse response = new RestResponse();
                if (client != null)
                {
                  response = client.Execute(request);
                }
              return response;
            }
            return null;
        }
        public RestResponse GetAnonymousResponse(RestRequest request)
        {
            RestResponse response = new RestResponse();
            if (client != null)
            {
                response = client.Execute(request);
            }
            return response;
        }
        public RestResponse Authenticate(AppUserModel usr)
        {
            
            RestResponse response = new RestResponse();
            try
            {
                var request = new RestRequest();
                request.Method = Method.Post;
                request.Resource = WebApiUri + "/Account/Token";
                request.AddJsonBody(usr);
                response = client.Execute(request);
            }
            catch(Exception ex)
            {

            }
            return response;
        }
    }
}

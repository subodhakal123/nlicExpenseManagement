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
        //private readonly IClaimsService _claimService;
        public WebApiService(IPrincipal principal)
        {
            if(User != null)
            {
                var abc = User.Identity;
            }
            
            identity = principal as ClaimsPrincipal;
            client = new RestClient();
        }

        public RestResponse GetResponse(RestRequest request)
        {
            //var claims = User.Claims;
            //if (identity.FindFirst("AccessToken") != null)
            //{
            //
            //    var AccessToken = identity.FindFirst("AccessToken").Value;
            //    request.AddParameter("Authorization", string.Format("Bearer {0}", AccessToken), ParameterType.HttpHeader);
                RestResponse response = new RestResponse();
            //    if (client != null)
            //    {
                    response = client.Execute(request);
            //    }
                return response;
            //}
            //return null;
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
                request.Resource = "https://localhost:7250/api/Account/Token";
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

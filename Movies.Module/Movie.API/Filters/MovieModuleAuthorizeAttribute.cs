using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    public class MovieModuleAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)
                    && !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var rawCredentials = authHeader.Parameter;
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                    var split = credentials.Split(':');
                    var userName = split[0];
                    var password = split[1];

                    // Go back to Shawns Implementing an API in ASP.NET Web API to figure out security.
                }
            }

            HandleUnauthorized(actionContext);
        }

        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='MovieModule' location='http://webaddress/account/login'");
        }
    }
}
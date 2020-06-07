using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.IdentityModel.Claims;
using System.Threading.Tasks;

namespace AzureAdAuthentication
{
    public partial class Startup
    {
        public static string clientid = ConfigurationManager.AppSettings["clientId"];
        public static string appKey = ConfigurationManager.AppSettings["clientSecret"];

        public static string postRedirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
        public static string tanantId = ConfigurationManager.AppSettings["tanantId"];
        public static string adInstance = ConfigurationManager.AppSettings["adinstance"];
        public static string authority = adInstance + tanantId;
        string graphResourceId = "https://graph.windows.net";

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = clientid,
                Authority = authority,
                PostLogoutRedirectUri = postRedirectUrl,
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    RoleClaimType = "roles"
                },
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    AuthorizationCodeReceived = (context) =>
                    {
                        var code = context.Code;
                        ClientCredential credential = new ClientCredential(clientid, appKey);

                        string signnedinUser = context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                        Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new
                        Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority);

                        Task<AuthenticationResult> result = authContext.AcquireTokenByAuthorizationCodeAsync(code,
                            new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)), credential, graphResourceId);

                        return Task.FromResult(0);
                    }
                }

            });
        }
    }
}
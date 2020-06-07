using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(AzureAdAuthentication.Startup))]
namespace AzureAdAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }


}
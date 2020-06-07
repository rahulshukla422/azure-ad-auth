using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AzureAdAuthentication.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var userIdentity = ((ClaimsIdentity)User.Identity);
            var claims = userIdentity.Claims;
            return View();
        }
    }
}
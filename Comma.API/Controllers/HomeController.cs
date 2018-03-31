using Comma.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace Comma.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string verb)
        {
           

            return View();
        }
    }
}

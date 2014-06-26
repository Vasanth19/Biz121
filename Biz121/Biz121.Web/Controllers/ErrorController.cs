using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Biz121.Web.Controllers
{
    [RoutePrefix("error")]
    public class ErrorController : Controller
    {


        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [Route("401")]
        public ActionResult Accessdenied()
        {
            return View();  
        }

        [Route("404")]
        public ActionResult Notfound()
        {
            return View();
        }

    }
}

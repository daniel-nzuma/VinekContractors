using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tracking.Controllers
{
    public class CarTrackingController : Controller
    {
        [Authorize]
        public ActionResult CarTrackingPanel()
        {
            return View();
        }

        [Authorize]
        public ActionResult GeoFence()
        {
            return View();
        }

        [Authorize]
        public ActionResult Commands()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {
            return View();
        }
    }
}
using scpoc.website.ModelBuilders;
using scpoc.website.Models.SitecoreItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scpoc.website.Controllers
{
    public class FlightGridController : BaseController
    {
        // GET: FligthGrid
        public ActionResult Index()
        {
            FlightGridModelBuilder mb = new FlightGridModelBuilder();
            FligthGridModel model = mb.BuildModel();
            return View("~/Views/FlightGrid/FlightGrid.cshtml", model);
        }
    }
}
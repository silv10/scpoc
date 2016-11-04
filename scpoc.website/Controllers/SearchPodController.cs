using scpoc.website.ModelBuilders;
using scpoc.website.Models.SitecoreItems;
using Sitecore.Data.Items;
using System.Web.Mvc;

namespace scpoc.website.Controllers
{
    public class SearchPodController : BaseController
    {
        public ActionResult Index()
        {
            Item ds = GetDatasourceItem();
            ds.Fields.ReadAll();
            SearchPodModelBuilder sb = new SearchPodModelBuilder();
            SearchPodModel model = sb.BuildModel(ds);
            return View("~/Views/SearchPod/SearchPod.cshtml", model);
        }
    }
}
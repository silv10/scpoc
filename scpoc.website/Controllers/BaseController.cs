using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System.Web.Mvc;

namespace scpoc.website.Controllers
{
    public abstract class BaseController : Controller
    { 
        public Item GetDatasourceItem()
        {
            var datasourceId = RenderingContext.Current.Rendering.DataSource;
            return ID.IsID(datasourceId) ? Context.Database.GetItem(datasourceId, Context.Language) : null;
        }
    }
}
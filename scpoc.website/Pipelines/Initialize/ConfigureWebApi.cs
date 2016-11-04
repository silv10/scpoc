using Sitecore.Pipelines;
using System.Web.Http;

namespace scpoc.website.Pipelines.Initialize
{
    public class ConfigureWebApi
    {
        public void Process(PipelineArgs args)
        {
            GlobalConfiguration.Configure(Configure);
        }
        protected void Configure(HttpConfiguration configuration)
        {
            var routes = configuration.Routes;
            routes.MapHttpRoute("ContentApi", "sitecore/api/content/{id}", new
            {
                controller = "ContentApi",
                action = "Get"
            });
        }
    }
}
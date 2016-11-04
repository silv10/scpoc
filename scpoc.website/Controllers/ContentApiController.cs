using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using System.Collections.Generic;
using System.Linq;
using scpoc.data.CustomApi;

namespace scpoc.website.Controllers
{
    public class ContentApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            Item contentItem = null;
            SitecoreControl result = new SitecoreControl();

            if (!string.IsNullOrEmpty(id))
                contentItem = Sitecore.Context.Database.GetItem(id);

            if (contentItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var xmlRendering = contentItem.Fields[Sitecore.FieldIDs.FinalLayoutField].Value;
            LayoutDefinition layout = LayoutDefinition.Parse(xmlRendering);
            DeviceDefinition device = layout.GetDevice(Sitecore.Context.Device.ID.ToString());

            if (device.DynamicProperties.Any())
            {
                var layoutID = device.DynamicProperties.Where(r => r.Name == "s:l").FirstOrDefault().Value;
                var layoutItem = Sitecore.Context.Database.GetItem(layoutID);

                result.Name = layoutItem.Name;
                result.Path = layoutItem.Fields["Path"].Value;
                result.Type = layoutItem.TemplateName;
            }

            List<PlaceholderSettings> phResult = new List<PlaceholderSettings>();
            
            if (device.Placeholders != null && device.Placeholders.Count > 0)
            {
                foreach (PlaceholderDefinition ph in device.Placeholders)
                {
                    List<ControlSettings> ctrlResult = new List<ControlSettings>(); 

                    PlaceholderSettings phSetting = new PlaceholderSettings();
                    phSetting.Name = ph.DynamicProperties.Where(r => r.Name == "s:key").FirstOrDefault().Value;

                    foreach (RenderingDefinition rd in device.Renderings.ToArray())
                    {
                        if (rd!= null && rd.DynamicProperties != null && rd.DynamicProperties.Count() >0 && rd.DynamicProperties.Where(r => r.Name == "s:ph").Any() && rd.DynamicProperties.Where(r => r.Name == "s:ph").FirstOrDefault().Value == phSetting.Name)
                        {
                            ControlSettings ctrlSetting = new ControlSettings();
                            var ctrlID = rd.DynamicProperties.Where(r => r.Name == "s:id").FirstOrDefault().Value;
                            var ctrlItem = Sitecore.Context.Database.GetItem(ctrlID);

                            if (ctrlItem == null)
                                continue;

                            ctrlSetting.Name = ctrlItem.Name;
                            //ctrlSetting.path = ctrlItem.Fields["Path"].Value;
                            ctrlSetting.Type = ctrlItem.TemplateName;

                            List<ControlDataSources> cdsResult = new List<ControlDataSources>();
                            if (rd.DynamicProperties.Where(r => r.Name == "s:ds").Any())
                            {
                                ControlDataSources cds = new ControlDataSources();
                                var dsID = rd.DynamicProperties.Where(r => r.Name == "s:ds").FirstOrDefault().Value;
                                var dsItem = Sitecore.Context.Database.GetItem(dsID);

                                if (dsItem == null)
                                    continue;

                                cds.Name = dsItem.Name;
                                cds.Path = dsItem.Paths.ContentPath;
                                cds.Template = dsItem.TemplateName;

                                List<ItemFields> fldResult = new List<ItemFields>();
                                dsItem.Fields.ReadAll();

                                IEnumerable<TemplateFieldItem> allFields = GetDataFields(dsItem.Template);

                                foreach (TemplateFieldItem field in allFields)
                                {
                                    ItemFields fld = new ItemFields
                                    {
                                        Name = dsItem.Fields[field.ID].Name,
                                        Type = dsItem.Fields[field.ID].Type,
                                        Value = dsItem.Fields[field.ID].Value
                                    };

                                    fldResult.Add(fld);
                                }

                                cds.Fields = new ItemFields[] { };
                                cds.Fields = fldResult.ToArray();

                                cdsResult.Add(cds);
                            }
                            ctrlSetting.DataSources = new ControlDataSources[] { };
                            ctrlSetting.DataSources = cdsResult.ToArray();

                            ctrlResult.Add(ctrlSetting);
                        }

                        phSetting.Controls = new ControlSettings[] { };
                        phSetting.Controls = ctrlResult.ToArray();
                    }
                    phResult.Add(phSetting);
                }
            }
            result.Placeholders = new PlaceholderSettings[] { };
            result.Placeholders = phResult.ToArray();
            return new JsonResult<SitecoreControl>(result, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        private static IEnumerable<TemplateFieldItem> GetDataFields(TemplateItem template)
        {
            var allFields = template.Fields;
            var dataFields = allFields.Where(x => x.Section.Name == "Data");
            return dataFields;
        }
    }
}
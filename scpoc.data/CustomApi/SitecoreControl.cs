using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scpoc.data.CustomApi
{
    public class SitecoreControl
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public PlaceholderSettings[] Placeholders { get; set; }
    }
}
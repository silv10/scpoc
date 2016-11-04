using scpoc.data;
using scpoc.website.Models.SitecoreItems;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace scpoc.website.ModelBuilders
{
    public class FlightGridModelBuilder
    {
        public FligthGridModel BuildModel()
        {
            return new FligthGridModel
            {
                Departure = GetDictionaryItem("departure"),
                Destination = GetDictionaryItem("destination"),
                LowFareLinkLabel = GetDictionaryItem("low fare link label"),
                MainTitle = GetDictionaryItem("main title")
            };
        }

        public static string GetDictionaryItem(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            string dictionaryValue = string.Empty;
            dictionaryValue = Translate.TextByLanguage(key, Context.Language);
            return dictionaryValue;
        }
    }
}
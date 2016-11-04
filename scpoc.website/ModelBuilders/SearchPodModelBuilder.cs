using scpoc.website.Models.SitecoreItems;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scpoc.website.ModelBuilders
{
    public class SearchPodModelBuilder
    {
        public SearchPodModel BuildModel(Item item)
        {
            return new SearchPodModel
            {
                DepartureLabel = item.Fields["Departure Label"].Value,
                DestinationLabel = item.Fields["Destination Label"].Value,
                DepartureDateLabel = item.Fields["Departure Date"].Value,
                ReturnDateLabel = item.Fields["Return Date"].Value,
                OneWayLabel = item.Fields["One Way Label"].Value,
                PassengerMix = item.Fields["Passenger Mix"].Value,
                NextStepLink = LinkManager.GetItemUrl(((Sitecore.Data.Fields.LinkField)item.Fields["Next Step"]).TargetItem, new UrlOptions { AlwaysIncludeServerUrl = true })
            };
        }
    }
}
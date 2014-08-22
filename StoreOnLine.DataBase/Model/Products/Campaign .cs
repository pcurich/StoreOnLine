using System;
using System.Collections.Generic;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.Products
{
    public class Campaign : DataBaseId
    {
        public String CampaignName { get; set; }
        public String CampaignDescription { get; set; }
        public IEnumerable<Imagen> CampaignPhoto { get; set; }
        public Imagen CampaignIcon { get; set; }

        public DateTime? CampaingStartTime { get; set; }
        public DateTime? CampaingEndTime { get; set; }


    }
}

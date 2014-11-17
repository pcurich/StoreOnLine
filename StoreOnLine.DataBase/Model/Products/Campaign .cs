using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsProduct;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.Products
{
    public class Campaign : DataBaseId
    {
        public String CampaignName { get; set; }
        public String CampaignDescription { get; set; }

        [XmlIgnore]
        public Imagen CampaignPhoto { get; set; }

        public DateTime? CampaingStartTime { get; set; }
        public DateTime? CampaingEndTime { get; set; }

        [XmlIgnore]
        public List<Product> Products { get; set; }
    }
}

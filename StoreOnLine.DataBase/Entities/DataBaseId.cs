using System;
using System.Web.Mvc;
 
using System.Xml.Serialization;

namespace StoreOnLine.DataBase.Entities
{

    public abstract class DataBaseId
    {
        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public bool Active { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public String AddUser { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public String UpdUser { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public DateTime? UpdDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public DateTime? AddDate { get; set; }

        protected DataBaseId()
        {
            AddDate = DateTime.Now;
            UpdDate = DateTime.Now;
            AddUser = "System";
            UpdUser = "System";
        }

    }
}

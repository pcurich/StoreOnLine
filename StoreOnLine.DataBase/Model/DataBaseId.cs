using System;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace StoreOnLine.DataBase.Model
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
        public long? UpdDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public long? AddDate { get; set; }

        protected DataBaseId()
        {
            AddDate = Util.DateTime.DateTimeConvert.GetDateTimeNow();
            UpdDate = Util.DateTime.DateTimeConvert.GetDateTimeNow();
            AddUser = "System";
            UpdUser = "System";
        }

    }
}

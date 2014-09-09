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
        public Boolean IsStatus { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public Boolean IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public String AddedUser { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public String ModificationUser { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public DateTime ModificationDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [XmlIgnore]
        public DateTime AddedDate { get; set; }

        protected DataBaseId()
        {
            AddedDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            AddedUser = "System";
            ModificationUser = "System";
        }

    }
}

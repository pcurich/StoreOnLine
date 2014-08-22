using System;
using System.Xml.Serialization;

namespace StoreOnLine.DataBase.Entities
{

    public abstract class DataBaseId
    {
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public Boolean IsStatus { get; set; }
        [XmlIgnore]
        public Boolean IsDeleted { get; set; }
        [XmlIgnore]
        public String AddedUser { get; set; }
        [XmlIgnore]
        public String ModificationUser { get; set; }
        [XmlIgnore]
        public DateTime ModificationDate { get; set; }
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

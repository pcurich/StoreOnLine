using System;

namespace StoreOnLine.DataBase.Entities
{
    public abstract class DataBaseId
    {
        public int Id { get; set; }

        public Boolean IsStatus { get; set; }
        public Boolean IsDeleted { get; set; }

        public String AddedUser { get; set; }
        public String ModificationUser { get; set; }

        public DateTime ModificationDate { get; set; }
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

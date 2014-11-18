namespace StoreOnLine.DataBase.Model.CmsShop
{
    public class ShopUrl: DataBaseId
    {
        public Shop Shop { get; set; }
        public int ShopId { get; set; }

        public string Domain { get; set; }
        public string DomainSsl { get; set; }
        public string PhysicalUrl { get; set; }
        public string VirtualUrl { get; set; }
        public bool Main { get; set; }


    }
}

using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.DataBase.Model.CmsShop
{
    public class ShopRol : DataBaseId//todo revisar si esto deberia ir o no 
    {
        public Shop Shop { get; set; }
        public int ShopId { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public string Name { get; set; }//Default
        public bool SharedCustumer { get; set; }
        public bool SharedOrder { get; set; }
        public bool SharedStock { get; set; }

    }
}

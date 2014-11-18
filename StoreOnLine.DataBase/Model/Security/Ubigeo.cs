namespace StoreOnLine.DataBase.Model.Security
{
    public class Ubigeo : DataBaseId
    {
        public string CodDpto { get; set; }
        public string CodProv { get; set; }
        public string CodDist { get; set; }
        public string NameUbiGeo { get; set; }

        public Ubigeo()
        {
        }

        public Ubigeo(int id, string codDpto, string codProv, string codDist, string nameUbiGeo)
        {
            Id = id;
            CodDpto = codDpto;
            CodDist = codDist;
            CodProv = codProv;
            NameUbiGeo = nameUbiGeo;
        }
    }

}

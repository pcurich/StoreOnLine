using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Ubigeo : DataBaseId
    {
        public string CodDpto { get; set; }
        public string CodProv { get; set; }
        public string CodDist { get; set; }
        public string NameUbiGeo { get; set; }

        //CodDpto CodProv CodDist
        //15		0			0	Lima  --Departamento
        //15		1			0	Lima  --Provincia
        //15		1			1	Lima  --distrito

    }

}

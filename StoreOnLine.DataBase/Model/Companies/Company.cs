
using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Model.Companies
{
    public class Company : DataBaseId
    {
        public string CompanyName { get; set; }
        public string CompanyActivity { get; set; }
        public string CompanyCif{ get; set; }
        public string CompanySecurityNumber { get; set; }

        public string CompanyDocumentRuc { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int ContactNumberId { get; set; }
        public ContactNumber ContactNumber { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public string CompanyType { get; set; }
        public string CompanyCode { get; set; }

        public bool HasSchedule { get; set; }
        public string StatusOfSchedule { get; set; }
        public List<Schedule> Schedules { get; set; }

    }
}

public enum CompanyType
{
    Internal, External
}

public enum StatusOfSchedule
{
    NoIniciada,//Valor por defecto
    ConRequerimientos,//El administrador registra un contrato
    SinRequerimientos,//El Supervisor termino el contrato
    EnProgreso, //El supervisor supervisa que se este ejecutando adecuadamente el progreso
    Terminado, //Ya se cumplio la demanda
    
}

using System;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Model.Companies
{
    public class ScheduleDetail : DataBaseId
    {
        public Schedule Schedule { get; set; }
        public int ScheduleId { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int TotalTime { get; set; }
        public string TypeOfTask { get; set; }
    }
}

public enum TypeOfTask
{
    Asignacion=1,
    Entrada=2,
    Salida=3,
    InicioRefrigerio=4,
    FinRefrigerio=5,
    Descanso=6,
    NoDefinido=7
}

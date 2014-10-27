using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreOnLine.Areas.Report.Models
{
    public class Day
    {
        public int Number { get; set; }
        public string AbbNameDay { get; set; }
        public string Activity { get; set; }
    }

}

public enum Leyend
{
    FL_D,//FERIADO LABORADO DIA
    FL_N,//FERIADO LABORADO NOCHE
    DES,//DESCANSO PROGRAMADO
    DT_D,//DESCANSO TRABAJADO DIA
    DT_N,//DESCANSO TRABAJADO NOCHE
    ROT,//ROTACION
    F,//FALTO
    P,//PERMISO
    S,//SUSPENCION
    DM,//DESCANSO MÉDICO
    VAC,//VACACIONES
    RN,//RENUNCIA
    D,//DIA
    N,//NOCHE
    AT,//ADELANTO DE TURNO
    PEG_D,//SERVICIO DE PEGADA DIA
    PEG_N,//SERVICIO DE PEGADA NOCHE
    L//LICENCIA
}
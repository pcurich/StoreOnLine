using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class ModuleSetting : DataBaseId
    {
        [Key]
        [Column(Order = 1)]
        public string SettingKey { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    //Esto es para parametros extras para un especifico modulo.
    //Digamos que tenemos una paguina llamada contact us
    //En esta paguina tenemos 2 forms 
    //1. Contact Sale Dept, 
    //2. Contact Service Dept.
    //ambos formularios se cargan desde el mismo modulo (contact Module)
    //entonces necesitamos parametros extras para enviar parametros a cada departamento 
    //Ex mail parameter
}


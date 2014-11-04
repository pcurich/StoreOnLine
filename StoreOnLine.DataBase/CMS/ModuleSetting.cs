using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// Ajuste de modulos, esto permite agregar parametros extras para un especifico modulo
    /// Digamos que tenemos una paguina llamada contact us
    /// En esta paguina tenemos 2 forms 
    /// 1. Contact Sale Dept, 
    /// 2. Contact Service Dept.
    /// ambos formularios se cargan desde el mismo modulo (contact Module)
    /// entonces necesitamos parametros extras para enviar parametros a cada departamento 
    /// Ex mail parameter
    /// </summary>
    public class ModuleSetting : DataBaseId
    {
        public string SettingKey { get; set; }

        public ModuleDefinition ModuleDefinition { get; set; }
        public int ModuleDefinitionId { get; set; }

        public string Value { get; set; }
        public string Description { get; set; }


    }
}




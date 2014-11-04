using System.Collections.Generic;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// Define un modulo a nivel de Area, Controlador y Accion 
    /// </summary>
    public class ModuleDefinition : DataBaseId
    {
        public string FeatureName { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string EditUrl { get; set; }
        public string Icon { get; set; }

        public List<ModuleSetting> ModuleSettings { get; set; }
    }
}

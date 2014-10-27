using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class ModuleDefinitions : DataBaseId
    {
        public string FeatureName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string EditUrl { get; set; }
        public string Icon { get; set; }
    }

    //Nuestro proposito es la carga dinamica de modulos entonces debenos de tener esto para decir que este modulo pertenece
    //a un controlador y a una accion 
}

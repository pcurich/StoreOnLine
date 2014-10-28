
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace StoreOnLine.Service
{
    //Esta es usada para hacer la url mas amigable es un mapping entre el nombre de la pagina y el id de la paguina
    //la paguina es cargada dinamicamente de la base de datos por su Id, esta clase encontrara el nombre de la paguina 
    //para generar un menu con una url amigable
    //Ejemplo Id=1 es homePage entonces generaremos un menu similar a http://yourdomain/homepage
    //Cuando el usuario haga click nosotros haremos uso de la clase PageRoute para encontrar el id 
    //de homePagepara cargar correctamente la paguina 
    public class SeoUrl
    {
        //private static CoreDbContext db = new CoreDbContext();
        private static readonly Dictionary<string, Dictionary<int, string>> MResourceCache = new Dictionary<string, Dictionary<int, string>>();
        
        private static SqlConnection _mConnection;
        private static SqlCommand _mCmdGetPropertyByTypeAndKey;

        public static string GetSeoUrl(int pageId)
        {
            string resourceValue = string.Empty;
            Dictionary<int, string> resCacheByCulture = null;
            
            //primero se revisa el cache 
            //busca en el diccionario para esta cultura
            //verifica en las entradas del diccionario la llave
            
            if (MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName))
            {
                resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];
                if (resCacheByCulture.ContainsKey(pageId))
                {
                    resourceValue = resCacheByCulture[pageId];
                }
            }

            // si no esta en el cache se va a base de datos
            if (!string.IsNullOrEmpty(resourceValue)) 
                return resourceValue;

            resourceValue = GetPropertyByTypeAndKey(pageId);

            //Se agrega el resultado en la cache 
            //busca en el diccionario para esta cultura
            //agrego este par key / valor a la entrada del diccionario interno 

            if (resourceValue == "-1") 
                return resourceValue;

            lock (typeof(ResourceString))
            {
                if (resCacheByCulture == null)
                {
                    resCacheByCulture = new Dictionary<int, string>();
                    MResourceCache.Add(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, resCacheByCulture);
                }
                resCacheByCulture.Add(pageId, resourceValue);
            }
            return resourceValue;
        }

        private static string GetPropertyByTypeAndKey(int pageId)
        {

            _mConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDBContext"].ConnectionString);

            //command to retrieve the resource the matches 
            //a specific type, culture and key
            _mCmdGetPropertyByTypeAndKey = new SqlCommand("SELECT propertyKey, seoValue FROM LocalizeProperties WHERE (propertyType=@propertyType) AND (CultureId=@CultureId) AND (propertyKey=@propertyKey)")
            {
                Connection = _mConnection
            };
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("propertyType", "page");
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("CultureId", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("propertyKey", pageId);

            // we should only get one back, but just in case, we'll iterate reader results
            var resources = new StringCollection();
            string resourceValue;
            //var _resource = db.resources.Where(r => r.propertyType == propertyType && r.CultureId == CultureInfo.CurrentCulture.TwoLetterISOLanguageName && r.propertyKey == propertyKey);
            try
            {
                _mConnection.Open();
                // get resources from the database
                using (SqlDataReader reader = _mCmdGetPropertyByTypeAndKey.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resources.Add(reader.GetString(reader.GetOrdinal("seoValue")));
                    }
                }
            }
            finally
            {
                _mConnection.Close();
            }
            //we should only get 1 back, this is just to verify the tables aren't incorrect
            switch (resources.Count)
            {
                case 0:
                    resourceValue = "-1";
                    break;
                case 1:
                    resourceValue = resources[0];
                    break;
                default:
                    throw new DataException(String.Format("Duplicated resource key {0}.{1}", pageId,""));
            }
            return resourceValue;
        }

        public static void RemoveKey(int pageId)
        {
            var propertyKeys = pageId;
            // check the cache first
            // find the dictionary for this culture
            // check for the inner dictionary entry for this key
            if (!MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)) 
                return;

            var resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];
            if (resCacheByCulture.ContainsKey(propertyKeys))
            {
                resCacheByCulture.Remove(propertyKeys);
            }
        }

        public static void UpdateKey(int pageId, string newValue)
        {
            var propertyKeys = pageId;
            if (!MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)) 
                return;

            var resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];
            if (resCacheByCulture.ContainsKey(propertyKeys))
            {
                resCacheByCulture[propertyKeys] = newValue;
            }
        }

        public static void Clear()
        {
            MResourceCache.Clear();
        }
    }
}

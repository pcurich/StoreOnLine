using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace StoreOnLine.Service.Business
{

    /// <summary>
    /// En un CMS las paginas son cargadas dinamicamente desde la base de datos por el Id de la paguina
    /// Imaginemos que tengo una paguina con Id=10 entonces tengo que ir al Url http://yourdomain/page?id=10 
    /// para cargar la paguina con el Id= 10, esta manera esta ok pero no es muy amigable
    /// Esta clase resuelve el problema y se revisara la paguina con el nombre cargado correctamente desde el id de la paguina
    /// Ejemplo: http://yourdomain/birthday entonces se captura la palabra cumpleaños para encontrarla desde su id
    /// </summary>
    public class PageRoute
    {
        //private static CoreDbContext db = new CoreDbContext();

        private static readonly Dictionary<string, Dictionary<string, int>> MResourceCache = new Dictionary<string, Dictionary<string, int>>();

        private static SqlConnection _mConnection;
        private static SqlCommand _mCmdGetPropertyByTypeAndKey;

        public static int GetPageId(string pagename)
        {
            var resourceValue = 0;
            Dictionary<string, int> resCacheByCulture = null;

            //primero reviso en la cache
            //buscar en el diccionario por la cultura
            //verifica en las entradas del diccionario la llave

            if (MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName))
            {
                resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];
                if (resCacheByCulture.ContainsKey(pagename))
                {
                    resourceValue = resCacheByCulture[pagename];
                }
            }

            // si no esta en el cache se va a base de datos
            if (resourceValue != 0)
                return resourceValue;

            resourceValue = GetPropertyByTypeAndKey(pagename);

            // add this result to the cache
            // find the dictionary for this culture
            // add this key/value pair to the inner dictionary
            if (resourceValue == 0)
                return resourceValue;

            lock (typeof(ResourceString))
            {
                if (resCacheByCulture == null)
                {
                    resCacheByCulture = new Dictionary<string, int>();
                    MResourceCache.Add(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, resCacheByCulture);
                }
                resCacheByCulture.Add(pagename, resourceValue);
            }
            return resourceValue;
        }

        private static int GetPropertyByTypeAndKey(string pagename)
        {

            _mConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDBContext"].ConnectionString);

            //command to retrieve the resource the matches 
            //a specific type, culture and key
            _mCmdGetPropertyByTypeAndKey = new SqlCommand("SELECT propertyKey, propertyValue FROM LocalizeProperties WHERE (propertyType=@propertyType) AND (LanguageId=@LanguageId) AND (SeoValue=@propertyKey)")
            {
                Connection = _mConnection
            };
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("propertyType", "page");
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("LanguageId", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("propertyKey", pagename);

            // we should only get one back, but just in case, we'll iterate reader results
            var resources = new StringCollection();
            int resourceValue;
            //var _resource = db.resources.Where(r => r.propertyType == propertyType && r.LanguageId == CultureInfo.CurrentCulture.TwoLetterISOLanguageName && r.propertyKey == propertyKey);
            try
            {
                _mConnection.Open();
                // get resources from the database
                using (var reader = _mCmdGetPropertyByTypeAndKey.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resources.Add(reader.GetString(reader.GetOrdinal("propertyKey")));
                    }
                }
            }
            finally
            {
                _mConnection.Close();
            }
            //we should only get 1 back, this is just to verify the tables aren't incorrect
            if (resources.Count == 0)
            {
                resourceValue = 0;
            }
            else if (resources.Count == 1)
            {
                resourceValue = int.Parse(resources[0]);
            }
            else
            {
                // if > 1 row returned, log an error, we shouldn't have > 1 value for a propertyKey!
                throw new DataException(String.Format("Duplicated resource key {0}.{1}", pagename, ""));
            }
            return resourceValue;
        }
        public static void RemoveKey(string pagename)
        {
            var propertyKeys = pagename;
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

        public static void UpdateKey(string pagename, int newValue)
        {
            string propertyKeys = pagename;
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

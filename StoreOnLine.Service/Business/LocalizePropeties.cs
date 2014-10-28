using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace StoreOnLine.Service.Business
{
    public class PropertyString
    {
        //private static CoreDbContext db = new CoreDbContext();
        private static readonly Dictionary<string, Dictionary<string, string>> MResourceCache = new Dictionary<string, Dictionary<string, string>>();
        private static SqlConnection _mConnection;
        private static SqlCommand _mCmdGetPropertyByTypeAndKey;

        public static String GetString(string propertyType, string propertyKey)
        {
            if (propertyKey == "-1")
            {
                return "Root";
            }

            string resourceValue = null;
            string propertyKeys = propertyType + "." + propertyKey;

            Dictionary<string, string> resCacheByCulture = null;

            // Resivo la cache primero
            // Encuentro en el diccionario para esta cultura
            // Revisar en la entrada en el diccionario para esta llave

            if (MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName))
            {
                resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];

                if (resCacheByCulture.ContainsKey(propertyKeys))
                {
                    resourceValue = resCacheByCulture[propertyKeys];
                }
            }

            // Si no esta en la cache ir a la base de datos

            if (resourceValue == null)
            {
                resourceValue = GetPropertyByTypeAndKey(propertyType, propertyKey);

                // add this result to the cache
                // find the dictionary for this culture
                // add this key/value pair to the inner dictionary

                lock (typeof(ResourceString))
                {
                    if (resCacheByCulture == null)
                    {
                        resCacheByCulture = new Dictionary<string, string>();
                        MResourceCache.Add(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, resCacheByCulture);
                    }
                    resCacheByCulture.Add(propertyKeys, resourceValue);
                }
            }

            return resourceValue;

        }


        private static string GetPropertyByTypeAndKey(string propertyType, string propertyKey)
        {
            _mConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDBContext"].ConnectionString);

            //command to retrieve the resource the matches
            //a specific type, culture and key

            _mCmdGetPropertyByTypeAndKey = new SqlCommand("SELECT propertyType, CultureId, propertyKey, propertyValue FROM LocalizeProperties WHERE (propertyType=@propertyType) AND (CultureId=@CultureId) AND (propertyKey=@propertyKey)")
            {
                Connection = _mConnection
            };
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("propertyType", propertyType);
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("CultureId", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            _mCmdGetPropertyByTypeAndKey.Parameters.AddWithValue("propertyKey", propertyKey);

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
                        resources.Add(reader.GetString(reader.GetOrdinal("propertyValue")));
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
                //if not exists return its key
                resourceValue = propertyType + "-" + propertyKey;

                //update new key to database
                var db = new DataBase.Entities.StoreOnLineContext();
                var lp = new DataBase.CMS.LocalizeProperties { CultureId = CultureInfo.CurrentCulture.TwoLetterISOLanguageName, PropertyType = propertyType, PropertyKey = propertyKey, PropertyValue = resourceValue, SeoValue = resourceValue };
                
                db.LocalizeProperties.Add(lp);
                db.SaveChanges();
            }

            else if (resources.Count == 1)
            {
                resourceValue = resources[0];
            }

            else
            {
                // if > 1 row returned, log an error, we shouldn't have > 1 value for a propertyKey!
                throw new DataException(String.Format("Duplicated resource key {0}.{1}", propertyType, propertyKey));
            }

            return resourceValue;

        }

        public static void RemoveKey(string propertyType, string propertyKey)
        {
            string propertyKeys = propertyType + "." + propertyKey;

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

        public static void UpdateKey(string propertyType, string propertyKey, string newValue)
        {
            var propertyKeys = propertyType + "." + propertyKey;

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
    //Esta clase es utilizada para localizar el nombre de la paguina, nombre del titulo 
}

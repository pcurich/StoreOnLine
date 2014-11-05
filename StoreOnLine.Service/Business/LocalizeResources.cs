using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace StoreOnLine.Service.Business
{
    public class ResourceString
    {
        //private static CoreDbContext db = new CoreDbContext();
        private static readonly Dictionary<string, Dictionary<string, string>> MResourceCache = new Dictionary<string, Dictionary<string, string>>();

        private static SqlConnection _mConnection;
        private static SqlCommand _mCmdGetResourceByTypeAndKey;


        public static String GetString(string resourceType, string resourceKey)
        {
            string resourceValue = null;
            string resourceKeys = resourceType + "." + resourceKey;
            Dictionary<string, string> resCacheByCulture = null;

            // check the cache first
            // find the dictionary for this culture
            // check for the inner dictionary entry for this key

            if (MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName))
            {
                resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];
                if (resCacheByCulture.ContainsKey(resourceKeys))
                {
                    resourceValue = resCacheByCulture[resourceKeys];
                }
            }

            // if not in the cache, go to the database
            if (resourceValue == null)
            {
                resourceValue = GetResourceByTypeAndKey(resourceType, resourceKey);
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
                    resCacheByCulture.Add(resourceKeys, resourceValue);
                }
            }

            return resourceValue;
        }

        private static string GetResourceByTypeAndKey(string resourceType, string resourceKey)
        {
            _mConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDBContext"].ConnectionString);
            //command to retrieve the resource the matches
            //a specific type, culture and key

            _mCmdGetResourceByTypeAndKey = new SqlCommand("SELECT resourceType, LanguageId, resourceKey, resourceValue FROM LocalizeResources WHERE (resourceType=@resourceType) AND (LanguageId=@LanguageId) AND (resourceKey=@resourceKey)")
            {
                Connection = _mConnection
            };
            _mCmdGetResourceByTypeAndKey.Parameters.AddWithValue("resourceType", resourceType);
            _mCmdGetResourceByTypeAndKey.Parameters.AddWithValue("LanguageId", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            _mCmdGetResourceByTypeAndKey.Parameters.AddWithValue("resourceKey", resourceKey);

            // we should only get one back, but just in case, we'll iterate reader results
            var resources = new StringCollection();

            string resourceValue;
            //var _resource = db.resources.Where(r => r.ResourceType == resourceType && r.LanguageId == CultureInfo.CurrentCulture.TwoLetterISOLanguageName && r.ResourceKey == resourceKey);

            try
            {
                _mConnection.Open();
                // get resources from the database

                using (SqlDataReader reader = _mCmdGetResourceByTypeAndKey.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("resourceValue")))
                        {
                            resources.Add(reader.GetString(reader.GetOrdinal("resourceValue")));
                        }
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
                resourceValue = resourceKey;

                //update new key to database
                var db = new DataBase.Entities.StoreOnLineContext();
                var lr = new DataBase.CMS.LocalizeResource { LanguageId = CultureInfo.CurrentCulture.TwoLetterISOLanguageName, ResourceType = resourceType, ResourceKey = resourceKey, ResourceValue = resourceValue };

                db.LocalizeResources.Add(lr);
                db.SaveChanges();

            }

            else if (resources.Count == 1)
            {
                resourceValue = resources[0];
            }
            else
            {
                // if > 1 row returned, log an error, we shouldn't have > 1 value for a resourceKey!
                throw new DataException(String.Format("Duplicated resource key {0}.{1}", resourceType, resourceKey));

            }
            return resourceValue;
        }

        public static void RemoveKey(string resourceType, string resourceKey)
        {
            var resourceKeys = resourceType + "." + resourceKey;
 
            if (MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName))
            {
                Dictionary<string, string>  resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];

                if (resCacheByCulture.ContainsKey(resourceKeys))
                {
                    resCacheByCulture.Remove(resourceKeys);
                }
            }
        }

        public static void UpdateKey(string resourceType, string resourceKey, string newValue)
        {
            string resourceKeys = resourceType + "." + resourceKey;

            if (MResourceCache.ContainsKey(CultureInfo.CurrentCulture.TwoLetterISOLanguageName))
            {
                var resCacheByCulture = MResourceCache[CultureInfo.CurrentCulture.TwoLetterISOLanguageName];

                if (resCacheByCulture.ContainsKey(resourceKeys))
                {
                    resCacheByCulture[resourceKeys] = newValue;
                }
            }
        }

        public static void Clear()
        {
            MResourceCache.Clear();
        }
    }
    //Esta clase es usada para localizar recursos desde la base de datos cuando el usuario cambia el luenguaje 
    //ex: label of textbox, button, sentences...
}

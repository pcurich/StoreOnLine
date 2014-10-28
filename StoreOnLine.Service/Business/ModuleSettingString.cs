using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StoreOnLine.Service.Business
{
    public class ModuleSettingString
    {
        private static readonly Dictionary<string, Dictionary<string, string>> MResourceCache = new Dictionary<string, Dictionary<string, string>>();
        private static SqlConnection _mConnection;
        private static SqlCommand _mCmdGetSettings;

        public static String GetString(string moduleId, string settingKey)
        {
            string settingValue = null;
            var settingKeys = moduleId + "." + settingKey;
            Dictionary<string, string> settingCacheByModuleId = null;
            //settingCacheBySiteId = null;
            // check the cache first
            // find the dictionary for this culture
            // check for the inner dictionary entry for this key
            if (MResourceCache.ContainsKey(moduleId))
            {
                settingCacheByModuleId = MResourceCache[moduleId];
                if (settingCacheByModuleId.ContainsKey(settingKeys))
                {
                    settingValue = settingCacheByModuleId[settingKeys];
                }
            }
            // if not in the cache, go to the database
            if (settingValue != null)
                return settingValue;

            settingValue = GetSettingByModuleAndKey(moduleId, settingKey);

            // add this result to the cache
            // find the dictionary for this culture
            // add this key/value pair to the inner dictionary
            lock (typeof(ResourceString))
            {
                if (settingCacheByModuleId == null)
                {
                    settingCacheByModuleId = new Dictionary<string, string>();
                    MResourceCache.Add(moduleId, settingCacheByModuleId);
                }
                settingCacheByModuleId.Add(settingKeys, settingValue);
            }
            return settingValue;
        }

        private static string GetSettingByModuleAndKey(string moduleId, string settingKey)
        {
            _mConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDBContext"].ConnectionString);

            //command to retrieve the resource the matches 
            //a specific type, culture and key
            _mCmdGetSettings = new SqlCommand("SELECT ModuleId, SettingKey, Value FROM ModuleSettings WHERE (ModuleId=@ModuleId) AND (SettingKey=@SettingKey)")
            {
                Connection = _mConnection
            };
            _mCmdGetSettings.Parameters.AddWithValue("ModuleId", moduleId);
            _mCmdGetSettings.Parameters.AddWithValue("SettingKey", settingKey);

            // we should only get one back, but just in case, we'll iterate reader results
            var resources = new StringCollection();
            string resourceValue;
            try
            {
                _mConnection.Open();
                // get resources from the database
                using (var reader = _mCmdGetSettings.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resources.Add(reader.GetString(reader.GetOrdinal("Value")));
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
                    resourceValue = moduleId + "." + settingKey;
                    break;
                case 1:
                    resourceValue = resources[0];
                    break;
                default:
                    throw new DataException(String.Format("Duplicated resource key {0}.{1}", moduleId, settingKey));
            }
            return resourceValue;
        }

        public static void RemoveKey(string moduleId, string settingKey)
        {
            var sitesettingKeys = moduleId + "." + settingKey;
            if (!MResourceCache.ContainsKey(moduleId))
                return;

            var resCacheBySiteId = MResourceCache[moduleId];
            if (resCacheBySiteId.ContainsKey(sitesettingKeys))
            {
                resCacheBySiteId.Remove(sitesettingKeys);
            }
        }

        public static void UpdateKey(string moduleId, string settingKey, string settingValue)
        {
            var sitesettingKeys = moduleId + "." + settingKey;
            if (!MResourceCache.ContainsKey(moduleId))
                return;

            var resCacheBySiteId = MResourceCache[moduleId];
            if (resCacheBySiteId.ContainsKey(sitesettingKeys))
            {
                resCacheBySiteId[sitesettingKeys] = settingValue;
            }
        }

        public static void Clear()
        {
            MResourceCache.Clear();
        }

        public static bool GetBoolean(string moduleId, string settingKey)
        {
            try
            {
                return Convert.ToBoolean(GetString(moduleId, settingKey));
            }
            catch
            {
                return false;
            }
        }

        public static int GetInt(string moduleId, string settingKey)
        {
            try
            {
                return Convert.ToInt16(GetString(moduleId, settingKey));
            }
            catch
            {
                return -1;
            }
        }
    }
}

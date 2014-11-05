using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StoreOnLine.Service.Business
{
    public class SiteSettingString
    {
        private static readonly Dictionary<string, string> MResourceCache = new Dictionary<string, string>();
        //private static Dictionary<string, string> settingCacheBySiteId;
        private static SqlConnection _mConnection;
        private static SqlCommand _mCmdGetSiteSettings;


        public static String GetString(string settingKey)
        {
            string settingValue = null;
            //settingCacheBySiteId = null;
            // check the cache first
            // find the dictionary for this culture
            // check for the inner dictionary entry for this key
            if (MResourceCache.ContainsKey(settingKey))
            {
                settingValue = MResourceCache[settingKey];
            }

            // if not in the cache, go to the database
            if (settingValue != null)
                return settingValue;

            settingValue = GetSettingBySiteAndKey(settingKey);
            settingValue = settingValue ?? "";
            // add this result to the cache
            // find the dictionary for this culture
            // add this key/value pair to the inner dictionary
            lock (typeof(ResourceString))
            {
                MResourceCache.Add(settingKey, settingValue);
            }
            return settingValue;
        }

        public static bool GetBoolean(string settingKey)
        {
            try
            {
                return Convert.ToBoolean(GetString(settingKey));
            }
            catch
            {
                return false;
            }
        }

        public static int GetInt(string settingKey)
        {
            try
            {
                return Convert.ToInt16(GetString(settingKey));
            }
            catch
            {
                return -1;
            }
        }

        public static String GetUserValue(string settingKey)
        {
            if (HttpContext.Current.Session[settingKey] != null)
                return HttpContext.Current.Session[settingKey].ToString();

            string getDataBaseValue;

            if (HttpContext.Current.Request.Browser.Cookies)
            {
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains(settingKey))
                {
                    var httpCookie = HttpContext.Current.Request.Cookies[settingKey];
                    if (httpCookie != null)
                        return httpCookie.Value;
                }

                getDataBaseValue = GetString(settingKey);
                SetUserValue(settingKey, getDataBaseValue);
                return getDataBaseValue;
            }

            getDataBaseValue = GetString(settingKey);
            SetUserValue(settingKey, getDataBaseValue);
            return getDataBaseValue;
        }

        public static void SetUserValue(string settingKey, string value)
        {
            HttpContext.Current.Session[settingKey] = value;

            if (!HttpContext.Current.Request.Browser.Cookies)
                return;

            var sitecookie = new HttpCookie(settingKey)
            {
                Value = value,
                Expires = DateTime.Now.AddDays(1)
            };

            HttpContext.Current.Response.Cookies.Add(sitecookie);
        }

        public static void RemoveUserValue(string settingKey)
        {
            HttpContext.Current.Session.Remove(settingKey);

            if (!HttpContext.Current.Request.Cookies.AllKeys.Contains(settingKey))
                return;

            var sitecookie = new HttpCookie(settingKey)
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            HttpContext.Current.Response.Cookies.Add(sitecookie);
        }

        public static void RemoveAllCookies()
        {
            var myCookies = HttpContext.Current.Request.Cookies.AllKeys;
            foreach (var httpCookie in myCookies.Select(cookie => HttpContext.Current.Response.Cookies[cookie]).Where(httpCookie => httpCookie != null))
            {
                httpCookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

        private static string GetSettingBySiteAndKey(string settingKey)
        {
            _mConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDBContext"].ConnectionString);

            //command to retrieve the resource the matches 
            //a specific type, culture and key
            _mCmdGetSiteSettings = new SqlCommand("SELECT SettingKey, SettingValue FROM SiteSettings WHERE SettingKey=@SettingKey")
            {
                Connection = _mConnection
            };
            _mCmdGetSiteSettings.Parameters.AddWithValue("SettingKey", settingKey);

            // we should only get one back, but just in case, we'll iterate reader results
            var resources = new StringCollection();
            string resourceValue;
            //var _resource = db.resources.Where(r => r.propertyType == propertyType && r.LanguageId == CultureInfo.CurrentCulture.TwoLetterISOLanguageName && r.propertyKey == propertyKey);
            try
            {
                _mConnection.Open();
                // get resources from the database
                using (var reader = _mCmdGetSiteSettings.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("SettingValue")))
                            resources.Add(reader.GetString(reader.GetOrdinal("SettingValue")));
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
                    resourceValue = "";
                    break;
                case 1:
                    resourceValue = resources[0];
                    break;
                default:
                    throw new DataException(String.Format("Duplicated resource key {0}", settingKey));
            }
            return resourceValue;
        }

        public static void RemoveKey(string settingKey)
        {
            if (MResourceCache.ContainsKey(settingKey))
            {
                MResourceCache.Remove(settingKey);
            }
        }

        public static void UpdateKey(string settingKey, string settingValue)
        {
            if (MResourceCache.ContainsKey(settingKey))
            {
                MResourceCache[settingKey] = settingValue;
            }
        }

        public static void Clear()
        {
            MResourceCache.Clear();
            //RemoveUserValue("SiteTheme");
            RemoveAllCookies();
            //settingCacheBySiteId.Clear();
        }
    }
}

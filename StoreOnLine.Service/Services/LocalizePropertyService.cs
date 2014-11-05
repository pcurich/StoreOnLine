using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;

namespace StoreOnLine.Service.Services
{
    public class LocalizePropertyService : BaseService
    {
        /// <summary>
        /// list properties by culture
        /// </summary>
        /// <param name="cultureId">culture id</param>
        /// <returns></returns>
        public IEnumerable<LocalizeProperty> GetPropertiesByCulture(int cultureId)
        {
            return Db.LocalizeProperties.Where(c => c.Id == cultureId).ToList();
        }

        public void AddProperty(LocalizeProperty e)
        {
            e.PropertyType = e.PropertyType.ToLower();
            e.SeoValue = e.SeoValue;
            Db.LocalizeProperties.Add(e);
            Db.SaveChanges();
        }

        public void UpdateProperty(LocalizeProperty e)
        {
            e.SeoValue = e.SeoValue;
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
            //PropertyString.UpdateKey(e.PropertyType, e.PropertyKey,e.PropertyValue);
            //because linq still cache so we have to remove
            PropertyString.RemoveKey(e.PropertyType, e.PropertyKey);
        }

        public void Updateinline(string type, string id, string value)
        {
            var lenguage =
                Db.Languages.FirstOrDefault(
                    o => o.TwoLetterIsoLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            var e = Db.LocalizeProperties.SingleOrDefault(p => p.LanguageId == lenguage .Id && p.PropertyType == type && p.PropertyKey == id);
            if (e != null)
            {
                e.PropertyValue = value;
                Db.SaveChanges();
            }
            else
                AddProperty(new LocalizeProperty { LanguageId = lenguage.Id, PropertyType = type, PropertyKey = id, PropertyValue = value });
            //PropertyString.UpdateKey(type, id, value);
            //because linq still cache so we have to remove
            PropertyString.RemoveKey(type, id);
        }

        public void DeleteByKey(string type, string id)
        {
            var lenguage =
                Db.Languages.FirstOrDefault(
                    o => o.TwoLetterIsoLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            var l = Db.LocalizeProperties.SingleOrDefault(p => p.LanguageId == lenguage.Id  && p.PropertyType == type && p.PropertyKey == id);
            Db.LocalizeProperties.Remove(l);
            Db.SaveChanges();

            if (l != null)
                PropertyString.RemoveKey(l.PropertyType, l.PropertyKey);
        }

        public void Delete(LocalizeProperty e)
        {
            LocalizeProperty l = Db.LocalizeProperties.SingleOrDefault(p => p.LanguageId == e.LanguageId && p.PropertyType == e.PropertyType && p.PropertyKey == e.PropertyKey);
            Db.LocalizeProperties.Remove(l);
            Db.SaveChanges();

            if (l != null)
                PropertyString.RemoveKey(l.PropertyType, l.PropertyKey);
        }

        public void pageName_Proerties_Add(int id, string name, string seoValue)
        {
            var lenguage =
                Db.Languages.FirstOrDefault(
                    o => o.TwoLetterIsoLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            var p = new LocalizeProperty
            {
                PropertyType = "page",
                PropertyKey = id.ToString(CultureInfo.InvariantCulture),
                PropertyValue = name,
                SeoValue = string.IsNullOrEmpty(seoValue) ? name : seoValue,
                LanguageId = lenguage.Id
            };
            Db.LocalizeProperties.Add(p);
            Db.SaveChanges();
        }

        public void pageName_Proerties_Update(int id, string name, string seoValue)
        {
            var lenguage =
                Db.Languages.FirstOrDefault(
                    o => o.TwoLetterIsoLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            var propertyId = id.ToString(CultureInfo.InvariantCulture);
            var l = Db.LocalizeProperties.SingleOrDefault(p => p.LanguageId == lenguage.Id && p.PropertyType.ToLower() == "page" && p.PropertyKey == propertyId);
            if (l != null)
            {
                l.PropertyValue = name;
                l.SeoValue = string.IsNullOrEmpty(seoValue) ? name : seoValue;
                Db.SaveChanges();
                PropertyString.RemoveKey(l.PropertyType, l.PropertyKey);
            }
            else
            {
                pageName_Proerties_Add(id, name, seoValue);
            }
        }

        public void pageName_Proerties_Delete(int id)
        {
            string propertyId = id.ToString(CultureInfo.InvariantCulture);
            var l = Db.LocalizeProperties.Where(p => p.PropertyType.ToLower() == "page" && p.PropertyKey == propertyId);
            foreach (var c in l)
            {
                PropertyString.RemoveKey(c.PropertyType, c.PropertyKey);
                Db.LocalizeProperties.Remove(c);
            }
            Db.SaveChanges();
        }

        public void moduleTitle_Proerties_Add(int id, string name)
        {
            var lenguage =
                Db.Languages.FirstOrDefault(
                    o => o.TwoLetterIsoLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            var p = new LocalizeProperty
            {
                PropertyType = "module",
                PropertyKey = id.ToString(CultureInfo.InvariantCulture),
                PropertyValue = name ?? "Title",
                SeoValue = name,
                LanguageId = lenguage.Id
            };
            Db.LocalizeProperties.Add(p);
            Db.SaveChanges();
        }

        public void moduleTitle_Proerties_Update(int id, string name)
        {
            var lenguage =
                Db.Languages.FirstOrDefault(
                    o => o.TwoLetterIsoLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            string propertyId = id.ToString(CultureInfo.InvariantCulture);
            LocalizeProperty l = Db.LocalizeProperties.SingleOrDefault(p => p.LanguageId == lenguage.Id && p.PropertyType.ToLower() == "module" && p.PropertyKey == propertyId);
            if (l != null)
            {
                l.PropertyValue = name;
                l.SeoValue = name;
                Db.SaveChanges();
                PropertyString.RemoveKey(l.PropertyType, l.PropertyKey);
            }
            else
                moduleTitle_Proerties_Add(id, name);
        }

        public void moduleTitle_Proerties_Delete(int id)
        {
            var propertyId = id.ToString(CultureInfo.InvariantCulture);
            var l = Db.LocalizeProperties.Where(p => p.PropertyType.ToLower() == "module" && p.PropertyKey == propertyId);
            foreach (var c in l)
            {
                PropertyString.RemoveKey(c.PropertyType, c.PropertyKey);
                Db.LocalizeProperties.Remove(c);
            }
            Db.SaveChanges();
        }
    }
}




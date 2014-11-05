using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class LocalizeResourceservice : BaseService
    {
        private const string Cacheid = "resource-{0}";
        private int languageId;

        public LocalizeResourceservice()
        {
            var language =
                Db.Languages.FirstOrDefault(
                    o => o.TwoLetterIsoLanguageName == CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            languageId = language.Id;
        }

        public IEnumerable<LocalizeResource> GetResourceAll()
        {
            Key = string.Format(Cacheid, "all");
            return Db.LocalizeResources.ToList();
        }

        public IEnumerable<LocalizeResource> GetResourceByCulture(int languageId)
        {
            Key = string.Format(Cacheid, languageId);
            return Db.LocalizeResources.Where(c => c.LanguageId == languageId).ToList();
        }

        public void AddResource(LocalizeResource e)
        {
            if (
                Db.LocalizeResources.Any(
                    c =>
                        c.LanguageId == e.LanguageId &&
                        c.ResourceType == e.ResourceType &&
                        c.ResourceKey == e.ResourceKey)
                )
                return;

            Db.LocalizeResources.Add(e);
            Db.SaveChanges();
        }

        public void UpdateResource(LocalizeResource update)
        {
            Db.Entry(update).State = EntityState.Modified;
            Db.SaveChanges();
            ResourceString.UpdateKey(update.ResourceType, update.ResourceKey, update.ResourceValue);
        }

        public void Updateinline(string type, string id, string value)
        {
            var e =
                Db.LocalizeResources.SingleOrDefault(
                    p => p.LanguageId == languageId &&
                         p.ResourceType == type && p.ResourceKey == id);

            if (e != null)
            {
                e.ResourceValue = value;
                Db.SaveChanges();
            }
            else
            {
                AddResource(new LocalizeResource
                {
                    LanguageId = languageId,
                    ResourceKey = id,
                    ResourceType = type,
                    ResourceValue = value
                });
            }

            ResourceString.UpdateKey(type, id, value);
        }

        public void DeleteByKey(string type, string id)
        {
            var l =
                Db.LocalizeResources.SingleOrDefault(
                    p =>
                        p.LanguageId == languageId && p.ResourceType == type &&
                        p.ResourceKey == id);
            Db.LocalizeResources.Remove(l);
            Db.SaveChanges();

            if (l != null) 
                ResourceString.RemoveKey(l.ResourceType, l.ResourceKey);
        }

        public void Delete(LocalizeResource resource)
        {
            var l =
                Db.LocalizeResources.SingleOrDefault(
                    p =>
                        p.LanguageId == resource.LanguageId && p.ResourceType == resource.ResourceType &&
                        p.ResourceKey == resource.ResourceKey);
            Db.LocalizeResources.Remove(l);
            Db.SaveChanges();
            ResourceString.RemoveKey(resource.ResourceType, resource.ResourceKey);
        }

        public void ClearResourceCache()
        {
            Cache.Remove(string.Format(Cacheid, CultureInfo.CurrentCulture.TwoLetterISOLanguageName));
        }
    }
}

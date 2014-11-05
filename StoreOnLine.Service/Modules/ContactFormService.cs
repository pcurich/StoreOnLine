using StoreOnLine.DataBase.Modules;
using StoreOnLine.Service.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Service.Modules
{
    public class ContactFormService : BaseService
    {
        private const string Cacheid = "ContactForms";

        public IEnumerable<ContactForm> GetContactFormBySiteId()
        {
            return Db.ContactForms.ToList();
        }

        public void Add(ContactForm e)
        {
            e.SendDate = DateTime.Now;
            e.ParentId = e.ParentId != 0 ? e.ParentId : 0;
            Db.ContactForms.Add(e);
        }

        public void Update(ContactForm e)
        {
            //e.SiteId = SiteId;
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Delete(ContactForm e)
        {
            ContactForm l = Db.ContactForms.SingleOrDefault(p => p.Id == e.Id);
            Db.ContactForms.Remove(l);
            Db.SaveChanges();
        }

        public void ClearCache()
        {
            Cache.Remove(Cacheid);
        }
    }
}

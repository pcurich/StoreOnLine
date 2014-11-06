//using StoreOnLine.DataBase.Modules;
//using StoreOnLine.Service.Business;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;

//namespace StoreOnLine.Service.Services
//{
//    public class ContactFormService : BaseService
//    {
//        private const string Cacheid = "ContactForms";

//        public IEnumerable<ContactForm> GetContactFormBySiteId()
//        {
//            return Db.contactforms.ToList();
//        }

//        public void Add(ContactForm e)
//        {
//            e.SendDate = DateTime.Now;
//            e.ParentId = e.ParentId != 0 ? e.ParentId : 0;
//            Db.contactforms.Add(e);
//        }

//        public void Update(ContactForm e)
//        {
//            //e.SiteId = SiteId;
//            Db.Entry(e).State = EntityState.Modified;
//            Db.SaveChanges();
//        }

//        public void Delete(ContactForm e)
//        {
//            ContactForm l = Db.contactforms.SingleOrDefault(p => p.Id == e.Id);
//            Db.contactforms.Remove(l);
//            Db.SaveChanges();
//        }

//        public void clearCache()
//        {
//            Cache.Remove(Cacheid);
//        }
//    }
//}
